using Microsoft.EntityFrameworkCore.Storage;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Models;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class OrderService(SalesManagementDbContext _dbContext) : IOrderService
{
    public async Task CreateOrder(OrderModel orderModel)
    {
        using (var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                Order order = new()
                {
                    OrderDateTime = DateTime.Now,
                    ClientId = orderModel.ClientId,
                    EmployeeId = 9,
                    Price = orderModel.OrderItems.Sum(o => o.Price),
                    Qty = orderModel.OrderItems.Sum(o => o.Qty)
                };

                var addedOrder = await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                int orderId = addedOrder.Entity.Id;

                var orderItemsToAdd = ReturnOrderItemsWithOrderId(orderId, orderModel.OrderItems);
                _dbContext.AddRange(orderItemsToAdd);

                await _dbContext.SaveChangesAsync();

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
}
