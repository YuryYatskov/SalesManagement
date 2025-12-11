using Microsoft.EntityFrameworkCore;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Models.ReportModels;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class SalesOrderReportService(SalesManagementDbContext _dbContext) : ISalesOrderReportService
{
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

    private Task<Employee> GetLoggedOnEmployee()
    {
        return Task.Run(() => new Employee { Id = 9 });
    }
}
