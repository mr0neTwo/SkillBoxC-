using System.ComponentModel.DataAnnotations;

namespace PhoneNotes.Models;

public sealed class UserRegisterModel
{
	[Required, MaxLength(256)]
	public string UserName { get; set; }
	
	[Required, DataType(DataType.Password)]
	public string Password { get; set; }
	
	[Required, DataType(DataType.Password), Compare(nameof(Password))]
	public string ConfirmPassword { get; set; }
}
