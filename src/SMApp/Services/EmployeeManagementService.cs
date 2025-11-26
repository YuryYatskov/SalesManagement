using Microsoft.EntityFrameworkCore;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Extensions;
using SMApp.Models;
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
}
