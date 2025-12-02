using Microsoft.EntityFrameworkCore;
using SMApp.Data;
using SMApp.Data.Entities;
using SMApp.Models;
using System;

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
            FirstName = employeeModel.FirstName.Trim(),
            LastName = employeeModel.LastName.Trim(),
            Email = employeeModel.Email.Trim(),
            DateOfBirth = employeeModel.DateOfBirth,
            Gender = employeeModel.Gender,
            ImagePath = employeeModel.Gender.ToUpper() == "MALE" ? "/Images/Profile/MaleDefault.jpg" : "/Images/Profile/FemaleDefault.jpg",
            EmployeeJobTitleId = employeeModel.EmployeeJobTitleId,
            ReportToEmpId = employeeModel.ReportToEmpId
        };
    }

    public static async Task<List<ProductModel>> Convert(this IQueryable<Product> products, SalesManagementDbContext _dbContext)
    {
        return await products
          .Join(_dbContext.ProductCategories,
              x => x.CategoryId,
              y => y.Id,
              (x, y) => new ProductModel
              {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagePath = x.ImagePath,
                Price = x.Price,
                CategoryId = x.CategoryId,
                CategoryName = y.Name
              })
          .ToListAsync();
    }
}
