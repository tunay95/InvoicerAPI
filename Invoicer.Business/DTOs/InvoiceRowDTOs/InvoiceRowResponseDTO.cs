namespace Invoicer.Business.DTOs.InvoiceRowDTOs;

public record InvoiceRowResponseDTO
{
	public Guid Id { get; set; }
	public string Service { get; set; } = string.Empty;
	public decimal Quantity { get; set; } = 1;
	public decimal Amount { get; set; }
	public decimal Sum { get; set; }


	public Guid InvoiceId { get; set; }
}
