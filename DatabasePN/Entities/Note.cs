namespace DatabasePN.Entities;

public sealed class Note
{
	public int Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string SecondName { get; set; } = string.Empty;
	public string ThirdName { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
}
