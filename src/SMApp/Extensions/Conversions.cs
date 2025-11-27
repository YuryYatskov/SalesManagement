using Microsoft.EntityFrameworkCore;
using SMApp.Data.Entities;
using SMApp.Models;

namespace SMApp.Extensions;

public static class Conversions
{
    public static async Task<List<EmployeeModel>> Convert(this IQueryable<Employee> employees)
    {
        return await employees.Select(x => new EmployeeModel {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            Gender = x.Gender,
            ImagePath = x.ImagePath,
            ReportToEmpId = x.ReportToEmpId,
            EmployeeJobTitleId = x.EmployeeJobTitleId
        }).ToListAsync();
    }

    public static Employee Convert(this EmployeeModel employeeModel)
    {
        return new Employee
        {
            FirstName = employeeModel.FirstName,
            LastName = employeeModel.LastName,
            Email = employeeModel.Email,
            DateOfBirth = employeeModel.DateOfBirth,
            Gender = employeeModel.Gender,
            ImagePath = employeeModel.Gender.ToUpper() == "MALE" ? "/Images/Profile/MaleDefault.jpg" : "/Images/Profile/FemaleDefault.jpg",
            EmployeeJobTitleId = employeeModel.EmployeeJobTitleId,
            ReportToEmpId = employeeModel.ReportToEmpId
        };
    }
}
