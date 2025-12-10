using Microsoft.EntityFrameworkCore;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Extensions;
using SMApp.Models;
using SMApp.Services.Contracts;
using System.Reflection;

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

            employees.Add(new ReportToModel { ReportToEmpId = null, ReportToName = "<None>" });

            return [.. employees.OrderBy(x => x.ReportToEmpId)];
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Employee> AddEmployee(EmployeeModel employeeModel)
    {
        try
        {
            Employee employeeToAdd = employeeModel.Convert();

            var result = await _dbContext.Employees.AddAsync(employeeToAdd);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task UpdateEmployee(EmployeeModel employeeModel)
    {
        try
        {
            var employeeToUpdate = await _dbContext.Employees.FindAsync(employeeModel.Id);

            if (employeeToUpdate != null)
            {
                employeeToUpdate.FirstName = employeeModel.FirstName.Trim();
                employeeToUpdate.LastName = employeeModel.LastName.Trim();
                employeeToUpdate.Email = employeeModel.Email.Trim();
                employeeToUpdate.Gender = employeeModel.Gender;
                employeeToUpdate.DateOfBirth = employeeModel.DateOfBirth;
                employeeToUpdate.ImagePath = employeeModel.ImagePath;
                employeeToUpdate.ReportToEmpId = employeeModel.ReportToEmpId;
                employeeToUpdate.EmployeeJobTitleId = employeeModel.EmployeeJobTitleId;

                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public async Task DeleteEmployee(int id)
    {
        try
        {
            var employeeToDelete = await _dbContext.Employees.FindAsync(id);

            if (employeeToDelete != null)
            {
                _dbContext.Employees.Remove(employeeToDelete);

                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
