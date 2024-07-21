using DatabasePN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneNoteApplication.Models;

namespace PhoneNoteAuthJWT.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class NoteController : Controller
{
	private readonly DatabaseContext _database;

	public NoteController(DatabaseContext database)
	{
		_database = database;
	}
	
	[HttpGet("list")]
	[Authorize]
	public async Task<ActionResult<List<Note>>> GetAllNotes()
	{
		List<Note> listAsync = await _database.Notes.ToListAsync();

		return new JsonResult(listAsync);
	}

	[HttpPost("add")]
	[Authorize]
	public async Task<ActionResult<int>> Add([FromBody] Note viewModel)
	{
		await _database.Notes.AddAsync(viewModel);
		await _database.SaveChangesAsync();

		return new JsonResult(viewModel.Id);
	}

	[HttpPost("edit")]
	[Authorize]
	public async Task<IActionResult> Edit([FromBody] Note viewModel)
	{
		Note? note = await _database.Notes.FindAsync(viewModel.Id);

		if (note is not null)
		{
			note.FirstName = viewModel.FirstName;
			note.SecondName = viewModel.SecondName;
			note.ThirdName = viewModel.ThirdName;
			note.PhoneNumber = viewModel.PhoneNumber;
			note.Address = viewModel.Address;
			note.Description = viewModel.Description;

			await _database.SaveChangesAsync();
		}

		return new JsonResult(string.Empty);
	}
	

	[HttpPost("delete")]
	[Authorize]
	public async Task<IActionResult> Delete([FromBody] Note viewModel)
	{
		Note? note = await _database.Notes.AsNoTracking().FirstOrDefaultAsync(note => note.Id == viewModel.Id);

		if (note is not null)
		{
			_database.Notes.Remove(note);
			await _database.SaveChangesAsync();
		}

		return new JsonResult(string.Empty);
	}
}
