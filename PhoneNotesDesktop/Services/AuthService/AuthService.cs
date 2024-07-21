using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using PhoneNoteAuthJWT.Models;
using UserLoginModel = PhoneNotes.Models.UserLoginModel;

namespace PhoneNotesDesktop.Services.AuthService;

public sealed class AuthService : IAuthService
{
	public string? Token => _token;
	public bool IsAuthorized { get; private set; }
	
	private readonly HttpClient _client;
	private string? _token;

	public AuthService()
	{
		_client = new HttpClient();
		_client.BaseAddress = new Uri("http://localhost:5124/api/");
		_client.DefaultRequestHeaders.Accept.Clear();
		_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
	}
	
	public async Task<bool> LoginAsync(string username, string password)
	{
		UserLoginModel loginModel = new()
		{
			UserName = username,
			Password = password
		};

		var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
		var response = await _client.PostAsync("auth/login", content);

		if (!response.IsSuccessStatusCode)
		{
			IsAuthorized = false;
			
			return false;
		}

		var responseContent = await response.Content.ReadAsStringAsync();
		_token = JsonConvert.DeserializeObject<LoginResponse>(responseContent)?.Token;
		IsAuthorized = true;
		
		return true;
	}
	
	public async Task Register(string username, string password, string confirmPassword)
	{
		await Task.Delay(1000);
	}
}
