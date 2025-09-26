using Invoicer.Core.Entities.Common;
using Invoicer.Core.Enums;

namespace Invoicer.Core.Entities;

public class Invoice : BaseEntity
{
	public DateTimeOffset StartDate { get; set; }
	public DateTimeOffset EndDate { get; set; }
	public decimal TotalSum { get; set; }
	public string? Comment { get; set; } = string.Empty;
	public InvoiceStatus Status { get; set; }
	public ICollection<InvoiceRow> InvoiceRows { get; set; } = new List<InvoiceRow>();


	public Guid CustomerId { get; set; }
	public Customer Customer { get; set; }
}
