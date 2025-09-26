namespace Invoicer.Business.DTOs.InvoiceRowDTOs;

public record CreateInvoiceRowDTO
{
	public string Service { get; set; } = string.Empty;
	public decimal Quantity { get; set; } = 1;
	public decimal Amount { get; set; }


	public Guid InvoiceId { get; set; }
}
