namespace Invoicer.Business.DTOs.InvoiceRowDTOs;

public record UpdateInvoiceRowDTO
{
	public string Service { get; set; } = string.Empty;
	public decimal Quantity { get; set; }
	public decimal Amount { get; set; }


	public Guid InvoiceId { get; set; }
}
