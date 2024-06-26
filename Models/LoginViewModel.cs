namespace EasyCheck.Models;

public class LoginViewModel
{
    public LoginModel Login { get; set; } = new LoginModel();
    public string? ReturnUrl { get; set; }
}
