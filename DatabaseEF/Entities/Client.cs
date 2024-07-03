namespace DatabaseEF.Entities;

public sealed class Client
{
	public int Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string ThirdName { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public List<Order> Orders { get; set; } = [];
}
