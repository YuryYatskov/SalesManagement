using Microsoft.EntityFrameworkCore;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Models.ReportModels;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class SalesOrderReportService(SalesManagementDbContext _dbContext) : ISalesOrderReportService
{
    //SR

    public async Task<List<GroupedFieldPriceModel>> GetEmployeePricePerMonthData()
    {
        try
        {
            var employee = await GetLoggedOnEmployee();
            var reportData = await(from s in _dbContext.SalesOrderReports
                                   where s.EmployeeId == employee.Id && s.OrderDateTime.Year == DateTime.Now.Year
                                   group s by s.OrderDateTime.Month into GroupedData
                                   orderby GroupedData.Key
                                   select new GroupedFieldPriceModel
                                   {
                                       GroupedFieldKey = (
                                           GroupedData.Key == 1 ? "Jan" :
                                           GroupedData.Key == 2 ? "Feb" :
                                           GroupedData.Key == 3 ? "Mar" :
                                           GroupedData.Key == 4 ? "Apr" :
                                           GroupedData.Key == 5 ? "May" :
                                           GroupedData.Key == 6 ? "Jun" :
                                           GroupedData.Key == 7 ? "Jul" :
                                           GroupedData.Key == 8 ? "Aug" :
                                           GroupedData.Key == 9 ? "Sep" :
                                           GroupedData.Key == 10 ? "Oct" :
                                           GroupedData.Key == 11 ? "Nov" :
                                           GroupedData.Key == 12 ? "Dec" :
                                           ""
                                       ),
                                       Price = Math.Round(GroupedData.Sum(o => o.OrderItemPrice), 2)

                                   }).ToListAsync();
            return reportData;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<GroupedFieldQtyModel>> GetQtyPerProductCategory()
    {
        try
        {
            var employee = await GetLoggedOnEmployee();
            var reportData = await(from s in _dbContext.SalesOrderReports
                                   where s.EmployeeId == employee.Id && s.OrderDateTime.Year == DateTime.Now.Year
                                   group s by s.OrderDateTime.Month into GroupedData
                                   orderby GroupedData.Key
                                   select new GroupedFieldQtyModel
                                   {
                                       GroupedFieldKey = (
                                           GroupedData.Key == 1 ? "Jan" :
                                           GroupedData.Key == 2 ? "Feb" :
                                           GroupedData.Key == 3 ? "Mar" :
                                           GroupedData.Key == 4 ? "Apr" :
                                           GroupedData.Key == 5 ? "May" :
                                           GroupedData.Key == 6 ? "Jun" :
                                           GroupedData.Key == 7 ? "Jul" :
                                           GroupedData.Key == 8 ? "Aug" :
                                           GroupedData.Key == 9 ? "Sep" :
                                           GroupedData.Key == 10 ? "Oct" :
                                           GroupedData.Key == 11 ? "Nov" :
                                           GroupedData.Key == 12 ? "Dec" :
                                           ""
                                       ),
                                       Qty = GroupedData.Sum(o => o.OrderItemQty)

                                   }).ToListAsync();
            return reportData;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<GroupedFieldQtyModel>> GetQtyPerMonthData()
    {
        try
        {
            var employee = await GetLoggedOnEmployee();
            var reportData = await(from s in _dbContext.SalesOrderReports
                                   where s.EmployeeId == employee.Id && s.OrderDateTime.Year == DateTime.Now.Year
                                   group s by s.OrderDateTime.Month into GroupedData
                                   orderby GroupedData.Key
                                   select new GroupedFieldQtyModel
                                   {
                                       GroupedFieldKey = (
                                           GroupedData.Key == 1 ? "Jan" :
                                           GroupedData.Key == 2 ? "Feb" :
                                           GroupedData.Key == 3 ? "Mar" :
                                           GroupedData.Key == 4 ? "Apr" :
                                           GroupedData.Key == 5 ? "May" :
                                           GroupedData.Key == 6 ? "Jun" :
                                           GroupedData.Key == 7 ? "Jul" :
                                           GroupedData.Key == 8 ? "Aug" :
                                           GroupedData.Key == 9 ? "Sep" :
                                           GroupedData.Key == 10 ? "Oct" :
                                           GroupedData.Key == 11 ? "Nov" :
                                           GroupedData.Key == 12 ? "Dec" :
                                           ""
                                       ),
                                       Qty = GroupedData.Sum(o => o.OrderItemQty)

                                   }).ToListAsync();
            return reportData;
        }
        catch (Exception)
        {

            throw;
        }
    }

    //TL
    public async Task<List<GroupedFieldPriceModel>> GetGrossSalesPerTeamMemberData()
    {
        try
        {
            var employee = await GetLoggedOnEmployee();

            List<int> teamMemberIds = await GetTeamMemberIds(3); // employee.Id);

            var reportData = await (from s in _dbContext.SalesOrderReports
                                    where teamMemberIds.Contains(s.EmployeeId)
                                    group s by s.EmployeeFirstName into GroupedData
                                    orderby GroupedData.Key
                                    select new GroupedFieldPriceModel
                                    {
                                        GroupedFieldKey = GroupedData.Key,
                                        Price = Math.Round(GroupedData.Sum(oi => oi.OrderItemPrice), 2)
                                    }).ToListAsync();
            return reportData;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<GroupedFieldQtyModel>> GetQtyPerTeamMemberData()
    {
        try
        {
            var employee = await GetLoggedOnEmployee();

            List<int> teamMemberIds = await GetTeamMemberIds(3); // employee.Id);
            var reportData = await (from s in _dbContext.SalesOrderReports
                                    where teamMemberIds.Contains(s.EmployeeId)
                                    group s by s.EmployeeFirstName into GroupedData
                                    orderby GroupedData.Key
                                    select new GroupedFieldQtyModel
                                    {
                                        GroupedFieldKey = GroupedData.Key,
                                        Qty = GroupedData.Sum(oi => oi.OrderItemQty)
                                    }).ToListAsync();
            return reportData;

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<GroupedFieldQtyModel>> GetTeamQtyPerMonthData()
    {
        try
        {
            var employee = await GetLoggedOnEmployee();

            List<int> teamMemberIds = await GetTeamMemberIds(3); // employee.Id);

            var reportData = await (from s in _dbContext.SalesOrderReports
                                    where teamMemberIds.Contains(s.EmployeeId) && s.OrderDateTime.Year == DateTime.Now.Year
                                    group s by s.OrderDateTime.Month into GroupedData
                                    orderby GroupedData.Key
                                    select new GroupedFieldQtyModel
                                    {
                                        GroupedFieldKey = (
                                            GroupedData.Key == 1 ? "Jan" :
                                            GroupedData.Key == 2 ? "Feb" :
                                            GroupedData.Key == 3 ? "Mar" :
                                            GroupedData.Key == 4 ? "Apr" :
                                            GroupedData.Key == 5 ? "May" :
                                            GroupedData.Key == 6 ? "Jun" :
                                            GroupedData.Key == 7 ? "Jul" :
                                            GroupedData.Key == 8 ? "Aug" :
                                            GroupedData.Key == 9 ? "Sep" :
                                            GroupedData.Key == 10 ? "Oct" :
                                            GroupedData.Key == 11 ? "Nov" :
                                            GroupedData.Key == 12 ? "Dec" :
                                            ""
                                        ),
                                        Qty = GroupedData.Sum(o => o.OrderItemQty)

                                    }).ToListAsync();
            return reportData;

        }
        catch (Exception)
        {

            throw;
        }
    }

    //SM
    public async Task<List<LocationProductCategoryModel>> GetQtyLocationProductCatData()
    {
        try
        {
            var reportData = await (from s in _dbContext.SalesOrderReports
                                    group s by s.RetailOutletLocation into GroupedData
                                    orderby GroupedData.Key
                                    select new LocationProductCategoryModel
                                    {
                                        Location = GroupedData.Key,
                                        MountainBikes = GroupedData.Where(p => p.ProductCategoryId == 1).Sum(o => o.OrderItemQty),
                                        RoadBikes = GroupedData.Where(p => p.ProductCategoryId == 2).Sum(o => o.OrderItemQty),
                                        Camping = GroupedData.Where(p => p.ProductCategoryId == 3).Sum(o => o.OrderItemQty),
                                        Hiking = GroupedData.Where(p => p.ProductCategoryId == 4).Sum(o => o.OrderItemQty),
                                        Boots = GroupedData.Where(p => p.ProductCategoryId == 5).Sum(o => o.OrderItemQty),

                                    }).ToListAsync();
            return reportData;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<GroupedFieldQtyModel>> GetQtyPerLocationData()
    {
        try
        {
            var reportData = await (from s in _dbContext.SalesOrderReports
                                    group s by s.RetailOutletLocation into GroupData
                                    orderby GroupData.Key
                                    select new GroupedFieldQtyModel
                                    {
                                        GroupedFieldKey = GroupData.Key,
                                        Qty = GroupData.Sum(o => o.OrderItemQty)
                                    }).ToListAsync();
            return reportData;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<MonthLocationModel>> GetQtyPerMonthLocationData()
    {
        try
        {
            var reportData = await (from s in _dbContext.SalesOrderReports
                                    where s.OrderDateTime.Year == DateTime.Now.Year
                                    group s by s.OrderDateTime.Month into GroupedData
                                    orderby GroupedData.Key
                                    select new MonthLocationModel
                                    {
                                        Month = (
                                           GroupedData.Key == 1 ? "Jan" :
                                           GroupedData.Key == 2 ? "Feb" :
                                           GroupedData.Key == 3 ? "Mar" :
                                           GroupedData.Key == 4 ? "Apr" :
                                           GroupedData.Key == 5 ? "May" :
                                           GroupedData.Key == 6 ? "Jun" :
                                           GroupedData.Key == 7 ? "Jul" :
                                           GroupedData.Key == 8 ? "Aug" :
                                           GroupedData.Key == 9 ? "Sep" :
                                           GroupedData.Key == 10 ? "Oct" :
                                           GroupedData.Key == 11 ? "Nov" :
                                           GroupedData.Key == 12 ? "Dec" :
                                           ""
                                       ),
                                        TX = GroupedData.Where(l => l.RetailOutletLocation == "TX").Sum(o => o.OrderItemQty),
                                        CA = GroupedData.Where(l => l.RetailOutletLocation == "CA").Sum(o => o.OrderItemQty),
                                        NY = GroupedData.Where(l => l.RetailOutletLocation == "NY").Sum(o => o.OrderItemQty),
                                        WA = GroupedData.Where(l => l.RetailOutletLocation == "WA").Sum(o => o.OrderItemQty)
                                    }).ToListAsync();
            return reportData;
        }
        catch (Exception)
        {

            throw;
        }
    }
    private async Task<List<int>> GetTeamMemberIds(int teamLeadId)
    {
        List<int> teamMemberIds = await _dbContext.Employees
                                    .Where(e => e.ReportToEmpId == teamLeadId)
                                    .Select(e => e.Id).ToListAsync();
        return teamMemberIds;

    }

    private Task<Employee> GetLoggedOnEmployee()
    {
        return Task.Run(() => new Employee { Id = 9 });
    }
}
