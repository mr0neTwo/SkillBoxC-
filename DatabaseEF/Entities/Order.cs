namespace DatabaseEF.Entities;

public sealed class Order
{
	public int Id { get; set; }
	public int ProductCode { get; set; }
	public string ProductName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public int ClientId { get; set; }
	
	public Client Client { get; set; }
}
