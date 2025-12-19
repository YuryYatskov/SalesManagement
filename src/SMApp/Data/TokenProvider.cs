namespace SMApp.Data;

public class TokenProvider
{
    public string XsrfToken { get; set; } = string.Empty;
    
    public string RefreshToken { get; set; } = string.Empty;
}
