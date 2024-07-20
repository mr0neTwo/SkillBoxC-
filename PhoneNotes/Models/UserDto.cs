using System.Security.Claims;

namespace PhoneNotes.Models;

public sealed class UserDto
{
	public string UserName { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public IList<RoleDto> Roles { get; set; }
	public IList<ClaimDto> Claims { get; set; }

	public UserDto()
	{
	}
}