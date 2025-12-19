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

    public static async Task<List<ClientModel>> Convert(this IQueryable<Client> clients, SalesManagementDbContext context)
    {
        return await (from c in clients
                      join r in context.RetailOutlets
                      on c.RetailOutletId equals r.Id
                      select new ClientModel
                      {
                          Id = c.Id,
                          Email = c.Email,
                          FirstName = c.FirstName,
                          LastName = c.LastName,
                          JobTitle = c.JobTitle,
                          PhoneNumber = c.PhoneNumber,
                          RetailOutletId = c.RetailOutletId,
                          RetailOutletLocation = r.Location,
                          RetailOutletName = r.Name

                      }).ToListAsync();
    }

    public static async Task<List<OrganisationModel>> ConvertToHierarchy(this IQueryable<Employee> employees, SalesManagementDbContext context)
    {
        return await (from e in employees
                      join t in context.EmployeeJobTitles
                      on e.EmployeeJobTitleId equals t.EmployeeJobTitleId
                      orderby e.Id
                      select new OrganisationModel
                      {
                          EmployeeId = e.Id.ToString(),
                          ReportsToId = e.ReportToEmpId != null ? e.ReportToEmpId.ToString() : "",
                          Email = e.Email,
                          FirstName = e.FirstName,
                          LastName = e.LastName,
                          ImagePath = e.ImagePath,
                          JobTitle = t.Name

                      }).ToListAsync();
    }

    public static async Task<List<AppointmentModel>> Convert(this IQueryable<Appointment> appointments)
    {
        return await (from a in appointments
                      select new AppointmentModel
                      {
                          Id = a.Id,
                          EmployeeId = a.EmployeeId,
                          Description = a.Description,
                          IsAllDay = a.IsAllDay,
                          RecurrenceId = a.RecurrenceId,
                          StartTime = a.StartTime,
                          EndTime = a.EndTime,
                          RecurrenceException = a.RecurrenceException,
                          RecurrenceRule = a.RecurrenceRule,
                          Location = a.Location,
                          Subject = a.Subject
                      }).ToListAsync();

    }

    public static Appointment Convert(this AppointmentModel appointmentModel)
    {
        return new Appointment
        {
            EmployeeId = appointmentModel.EmployeeId,
            Description = appointmentModel.Description,
            IsAllDay = appointmentModel.IsAllDay,
            RecurrenceId = appointmentModel.RecurrenceId,
            StartTime = appointmentModel.StartTime,
            EndTime = appointmentModel.EndTime,
            RecurrenceException = appointmentModel.RecurrenceException,
            RecurrenceRule = appointmentModel.RecurrenceRule,
            Location = appointmentModel.Location,
            Subject = appointmentModel.Subject
        };
    }

    public static async Task<Employee> GetEmployeeObject(
        this System.Security.Claims.ClaimsPrincipal user,
        SalesManagementDbContext context)
    {
        var emailAddress = user?.Identity?.Name;
        if (emailAddress is null)
            return new Employee();

        var employee = await context.Employees.Where(e => e.Email.ToLower() == emailAddress.ToLower()).SingleOrDefaultAsync();
        if (employee is null)
            return new Employee();

        return employee;
    }
}
