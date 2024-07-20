using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using PhoneNotes.Models;

namespace PhoneNotesDesktop.Services.AuthService;

public sealed class AuthService : IAuthService
{
	public string? Token => _token;
	public bool IsAuthorized { get; private set; }

	public UserDto User => _user;
	
	private readonly HttpClient _client;
	private UserDto? _user;
	private string? _token;

	public AuthService()
	{
		_client = new HttpClient();
		_client.BaseAddress = new Uri($"http://localhost:5094/apiaccount/");
		_client.DefaultRequestHeaders.Accept.Clear();
		_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
	}
	
	public async Task<bool> LoginAsync(string username, string password)
	{
		var loginModel = new
		{
			UserName = username,
			Password = password
		};

		var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
		var response = await _client.PostAsync("Login", content);

		if (!response.IsSuccessStatusCode)
		{
			IsAuthorized = false;
			
			return false;
		}

		var responseContent = await response.Content.ReadAsStringAsync();
		_token = JsonConvert.DeserializeObject<dynamic>(responseContent)?.token;
		IsAuthorized = true;
		
		return true;
	}

	public async Task<bool> Login(string username, string password)
	{
		var user = new
		{
			UserName = username,
			Password = password,
		};

		string json = JsonConvert.SerializeObject(user);
		var content = new StringContent(json, Encoding.UTF8, "application/json");

		HttpResponseMessage response = await _client.PostAsync("Login", content);
		
		Console.WriteLine($"StatusCode: [{response.StatusCode}]");

		if (response.IsSuccessStatusCode)
		{
			string result = await response.Content.ReadAsStringAsync();
			_user = JsonConvert.DeserializeObject<UserDto>(result);
			
			return true;
		}
		
		string errorResponse = await response.Content.ReadAsStringAsync();
		Console.WriteLine(errorResponse);
		
		return false;
	}

	public async Task Register(string username, string password, string confirmPassword)
	{
		await Task.Delay(1000);
	}
}
