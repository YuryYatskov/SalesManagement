using Microsoft.EntityFrameworkCore;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Extensions;
using SMApp.Models;
using SMApp.Models.Reports;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class EmployeeManagementService(SalesManagementDbContext _dbContext) : IEmployeeManagementService
{
    public async Task<List<EmployeeModel>> GetEmployees()
    {
        try
        {
            return await _dbContext.Employees.Convert();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<EmployeeJobTitle>> GetJobTitles()
    {
        try
        {
            return await _dbContext.EmployeeJobTitles.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<ReportToModel>> GetReportToEmployees()
    {
        try
        {
            var employees = await _dbContext.Employees
                .Join(_dbContext.EmployeeJobTitles
                        .Where(x => x.Name.ToUpper() == "TL"
                            || x.Name.ToUpper() == "SM"),
                    x => x.EmployeeJobTitleId,
                    y => y.EmployeeJobTitleId,
                    (x, y) => new ReportToModel
                    {
                        ReportToEmpId = x.Id,
                        ReportToName = x.FirstName + " " + x.LastName.Substring(0, 1).ToUpper() + ".",
                    })
                .ToListAsync();

            return employees;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
