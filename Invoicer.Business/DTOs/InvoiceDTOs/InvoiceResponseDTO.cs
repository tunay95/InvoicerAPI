using Invoicer.Business.DTOs.InvoiceRowDTOs;
using Invoicer.Core.Enums;

namespace Invoicer.Business.DTOs.InvoiceDTOs;

public class InvoiceResponseDTO
{
	public Guid Id { get; set; }
	public DateTimeOffset StartDate { get; set; }
	public DateTimeOffset EndDate { get; set; }
	public decimal TotalSum { get; set; }
	public string? Comment { get; set; } = string.Empty;
	public InvoiceStatus Status { get; set; }
	public IEnumerable<InvoiceRowResponseDTO>? InvoiceRows { get; set; } = new List<InvoiceRowResponseDTO>();

	public Guid CustomerId { get; set; }
}
