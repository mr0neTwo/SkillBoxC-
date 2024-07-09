namespace DatabaseEF.Entities;

public sealed class Student
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public bool Subscribe { get; set; }
}
