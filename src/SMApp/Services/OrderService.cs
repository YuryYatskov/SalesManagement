using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Data.Entities.Reports;
using SMApp.Extensions;
using SMApp.Models;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class OrderService(SalesManagementDbContext _dbContext,
    AuthenticationStateProvider _authenticationStateProvider) : IOrderService
{
    public async Task CreateOrder(OrderModel orderModel)
    {
        using (var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var employee = await GetLoggedOnEmployee();

                Order order = new()
                {
                    OrderDateTime = DateTime.Now,
                    ClientId = orderModel.ClientId,
                    EmployeeId = employee.Id,
                    Price = orderModel.OrderItems!.Sum(o => o.Price),
                    Qty = orderModel.OrderItems!.Sum(o => o.Qty)
                };

                var addedOrder = await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                int orderId = addedOrder.Entity.Id;

                var orderItemsToAdd = ReturnOrderItemsWithOrderId(orderId, orderModel.OrderItems!);
                _dbContext.AddRange(orderItemsToAdd);

                await _dbContext.SaveChangesAsync();

                await UpdateSalesOrderReportsTable(orderId, order);

                await dbContextTransaction.CommitAsync();
            }
            catch (Exception)
            {
                await dbContextTransaction.DisposeAsync();
                throw;
            }
        }
    }

    private static List<OrderItem> ReturnOrderItemsWithOrderId(int orderId, List<OrderItem> orderItems)
    {
        return [.. (from oi in orderItems
                select new OrderItem
                {
                    OrderId = orderId,
                    Price = oi.Price,
                    Qty = oi.Qty,
                    ProductId = oi.ProductId
                })];
    }

    private async Task UpdateSalesOrderReportsTable(int orderId, Order order)
    {
        try
        {
            List<SalesOrderReport> srItems = await (from oi in _dbContext.OrderItems
                                                    where oi.OrderId == orderId
                                                    select new SalesOrderReport
                                                    {
                                                        OrderId = orderId,
                                                        OrderDateTime = order.OrderDateTime,
                                                        OrderPrice = order.Price,
                                                        OrderQty = order.Qty,
                                                        OrderItemId = oi.Id,
                                                        OrderItemPrice = oi.Price,
                                                        OrderItemQty = oi.Qty,
                                                        EmployeeId = order.EmployeeId,
                                                        EmployeeFirstName = _dbContext.Employees.FirstOrDefault(e => e.Id == order.EmployeeId)!.FirstName,
                                                        EmployeeLastName = _dbContext.Employees.FirstOrDefault(e => e.Id == order.EmployeeId)!.LastName,
                                                        ProductId = oi.ProductId,
                                                        ProductName = _dbContext.Products.FirstOrDefault(p => p.Id == oi.ProductId)!.Name,
                                                        ProductCategoryId = _dbContext.Products.FirstOrDefault(p => p.Id == oi.ProductId)!.CategoryId,
                                                        ProductCategoryName = _dbContext.ProductCategories.FirstOrDefault(c => c.Id == _dbContext.Products.FirstOrDefault(p => p.Id == oi.ProductId)!.CategoryId)!.Name,
                                                        ClientId = order.ClientId,
                                                        ClientFirstName = _dbContext.Clients.FirstOrDefault(c => c.Id == order.ClientId)!.FirstName,
                                                        ClientLastName = _dbContext.Clients.FirstOrDefault(c => c.Id == order.ClientId)!.LastName,
                                                        RetailOutletId = _dbContext.Clients.FirstOrDefault(c => c.Id == order.ClientId)!.RetailOutletId,
                                                        RetailOutletLocation = _dbContext.RetailOutlets.FirstOrDefault(r => r.Id == _dbContext.Clients.FirstOrDefault(c => c.Id == order.ClientId)!.RetailOutletId)!.Location
                                                    }).ToListAsync();

            _dbContext.AddRange(srItems);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> SalesOrderReports()
    {
        bool task = false;
        try
        {
            var salesOrderReports = await _dbContext.SalesOrderReports.ToArrayAsync();
            _dbContext.SalesOrderReports.RemoveRange(salesOrderReports);

            var orders = await _dbContext.Orders.AsNoTracking().ToListAsync();
            foreach (var order in orders)
            {
                await UpdateSalesOrderReportsTable(order.Id, order);
            }
            task = true;
        }
        catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return task;
    }

    private async Task<Employee> GetLoggedOnEmployee()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return await user.GetEmployeeObject(_dbContext);
    }
}
