using SMApp.Data;
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
}
