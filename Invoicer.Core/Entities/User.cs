using Invoicer.Core.Entities.Common;

namespace Invoicer.Core.Entities;

public class User:BaseEntity
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string? Address { get; set; } = string.Empty;
	public string? PhoneNumber { get; set; } = string.Empty;

	public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();	

}
