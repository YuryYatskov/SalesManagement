namespace SMApp.Models;

public class ClientModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RetailOutletId { get; set; }
    public string RetailOutletName { get; set; } = string.Empty;
    public string RetailOutletLocation { get; set; } = string.Empty;
}
