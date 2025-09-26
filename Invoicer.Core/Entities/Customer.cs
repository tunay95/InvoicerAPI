using Invoicer.Core.Entities.Common;

namespace Invoicer.Core.Entities;

public class Customer : BaseEntity
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string? PhoneNumber { get; set; }
	public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

}
