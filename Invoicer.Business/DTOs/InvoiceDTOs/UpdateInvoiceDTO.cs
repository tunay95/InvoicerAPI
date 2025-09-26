using Invoicer.Core.Enums;

namespace Invoicer.Business.DTOs.InvoiceDTOs;

public class UpdateInvoiceDTO
{
	public DateTimeOffset? StartDate { get; set; }
	public DateTimeOffset? EndDate { get; set; }
	public string? Comment { get; set; } 
	//public InvoiceStatus? Status { get; set; }
	public Guid? CustomerId { get; set; }
}
