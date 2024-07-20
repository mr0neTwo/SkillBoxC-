namespace PhoneNotesDesktop.Services.AuthService;

public interface IAuthService
{
	string? Token { get; }

	Task<bool> Login(string username, string password);

	Task<bool> LoginAsync(string username, string password);

	Task Register(string username, string password, string confirmPassword);
}
