using System.Security.Claims;

namespace PhoneNotes.Models;

public sealed class RoleDto 
{
	public string Name { get; set; }
	public IList<ClaimDto> Claims { get; set; }

	public RoleDto()
	{
	}
}
