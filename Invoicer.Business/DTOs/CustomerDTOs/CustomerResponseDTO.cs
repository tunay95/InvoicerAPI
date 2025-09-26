using Invoicer.Business.DTOs.InvoiceDTOs;

namespace Invoicer.Business.DTOs.CustomerDTOs;

public record CustomerResponseDTO
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string? PhoneNumber { get; set; }

	public IEnumerable<InvoiceResponseDTO>? Invoices { get; set; } = new List<InvoiceResponseDTO>();
}
