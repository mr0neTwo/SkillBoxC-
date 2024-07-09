using System.Diagnostics;
using DatabasePN;
using DatabasePN.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneNotes.Models;

namespace PhoneNotes.Controllers;

public sealed class NoteController : Controller
{
	private DatabaseContext _database;

	public NoteController(DatabaseContext database)
	{
		_database = database;
		// _database.FillDefaultValues();
	}
	

	public async Task<IActionResult> List()
	{
		List<Note> listAsync = await _database.Notes.ToListAsync();

		return View(listAsync);
	}
	
	[HttpGet]
	public IActionResult Add()
	{
		Note note = new();
		
		return View(note);
	}
	
	[HttpPost]
	public async Task<IActionResult> Add(Note viewModel)
	{
		await _database.Notes.AddAsync(viewModel);
		await _database.SaveChangesAsync();

		return RedirectToAction("List", "Note");
	}
	
	[HttpPost]
	public async Task<IActionResult> Edit(Note viewModel)
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
		
		return RedirectToAction("List", "Note");
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		Note? note = await _database.Notes.FindAsync(id);

		return View(note);
	}
	
	[HttpGet]
	public async Task<IActionResult> Show(int id)
	{
		Note? note = await _database.Notes.FindAsync(id);

		return View(note);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	[HttpPost]
	public IActionResult Back()
	{
		return RedirectToAction("List", "Note");
	}
	
	[HttpPost]
	public async Task<IActionResult> Delete(Note viewModel)
	{
		Note? note = await _database.Notes
										  .AsNoTracking()
										  .FirstOrDefaultAsync(n => n.Id == viewModel.Id);

		if (note is not null)
		{
			_database.Notes.Remove(note);
			await _database.SaveChangesAsync();
		}
		
		return RedirectToAction("List", "Note");
	}
}
