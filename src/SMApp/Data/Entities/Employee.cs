using System.ComponentModel.DataAnnotations;

namespace SMApp.Data.Entities;

public class Employee
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int? ReportToEmpId { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public int EmployeeJobTitleId { get; set; }
}
