using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using PhoneNoteApplication.Models;
using PhoneNotesDesktop.Services.AuthService;

namespace PhoneNotesDesktop.Services.DataProvider;

public sealed class ApiDataProvider : IDataProvider
{
	private IAuthService _authService;
	private readonly HttpClient _client;

	public ApiDataProvider(IAuthService authService)
	{
		_authService = authService;
		_client = new HttpClient();
		
		_client.BaseAddress = new Uri("http://localhost:5094/api/");
		
		_client.DefaultRequestHeaders.Accept.Clear();
		_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authService.Token);

	}

	public async Task<List<Note>> GetAllNotes()
	{
		HttpResponseMessage response = await _client.GetAsync("getAllNotes");
		
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
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.Default, "application/json");
		await _client.PostAsync("Add", content);
	}

	public async Task EditNote(Note note)
	{
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		await _client.PostAsync("Edit", content);
	}

	public async Task DeleteNote(Note note)
	{
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		await _client.PostAsync("Delete", content);
	}
}
