namespace Invoicer.Business.DTOs.InvoiceDTOs;

public class CreateInvoiceDTO
{
	public DateTimeOffset StartDate { get; set; }
	public DateTimeOffset EndDate { get; set; }
	public string? Comment { get; set; } = string.Empty;
	public Guid CustomerId { get; set; }

}
