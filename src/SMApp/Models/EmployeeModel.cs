using System.ComponentModel.DataAnnotations;

namespace SMApp.Models;

public class EmployeeModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters or be less than {2} characters.", MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters or be less than {2} characters.", MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int? ReportToEmpId { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public int EmployeeJobTitleId { get; set; }
}
