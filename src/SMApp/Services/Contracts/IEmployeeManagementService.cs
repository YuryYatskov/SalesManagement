using SMApp.Data.Entities;
using SMApp.Models;
using SMApp.Models.Reports;

namespace SMApp.Services.Contracts;

public interface IEmployeeManagementService
{
    Task<List<EmployeeModel>> GetEmployees();

    Task<List<EmployeeJobTitle>> GetJobTitles();

    Task<List<ReportToModel>> GetReportToEmployees();

    Task<Employee> AddEmployee(EmployeeModel employeeModel);

    Task UpdateEmployee(EmployeeModel employeeModel);

    Task DeleteEmployee(int id);
}
