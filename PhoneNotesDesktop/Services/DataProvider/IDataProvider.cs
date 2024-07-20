
using PhoneNoteApplication.Models;

namespace PhoneNotesDesktop.Services.DataProvider;

public interface IDataProvider
{
	public Task<List<Note>> GetAllNotes();
	
	public Task AddNote(Note note);

	public Task EditNote(Note note);

	public Task DeleteNote(Note note);
}
