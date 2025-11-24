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
}
