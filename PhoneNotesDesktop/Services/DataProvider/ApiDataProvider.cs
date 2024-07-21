using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using PhoneNoteApplication.Models;
using PhoneNotesDesktop.Services.AuthService;
using Uri = System.Uri;

namespace PhoneNotesDesktop.Services.DataProvider;

public sealed class ApiDataProvider : IDataProvider
{
	private readonly HttpClient _client;
	private IAuthService _authService;

	public ApiDataProvider(IAuthService authService)
	{
		_authService = authService;
		_client = new HttpClient();
		
		_client.BaseAddress = new Uri("http://localhost:5124/api/");
		
		_client.DefaultRequestHeaders.Accept.Clear();
		_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	}

	public async Task<List<Note>> GetAllNotes()
	{
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, _authService.Token);
		HttpResponseMessage response = await _client.GetAsync("note/list");

		if (response.IsSuccessStatusCode == false)
		{
			return new List<Note>();
		}
		
		string json = await response.Content.ReadAsStringAsync();
		List<Note>? notes = JsonConvert.DeserializeObject<List<Note>>(json);

		if (notes == null)
		{
			return new List<Note>();
		}
		
		return notes;
	}

	public async Task AddNote(Note note)
	{
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, _authService.Token);
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.Default, "application/json");
		await _client.PostAsync("note/add", content);
	}

	public async Task EditNote(Note note)
	{
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		await _client.PostAsync("note/edit", content);
	}

	public async Task DeleteNote(Note note)
	{
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		await _client.PostAsync("note/delete", content);
	}
}
