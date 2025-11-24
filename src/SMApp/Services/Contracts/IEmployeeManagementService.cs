using SMApp.Models;

namespace SMApp.Services.Contracts;

public interface IEmployeeManagementService
{
    Task<List<EmployeeModel>> GetEmployees();


}
