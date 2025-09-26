using Invoicer.Business.DTOs.Base;
using Invoicer.Business.DTOs.InvoiceDTOs;

namespace Invoicer.Business.Services.Interfaces;

public interface IInvoiceService
{
	Task<PaginationResponseDTO<InvoiceResponseDTO?>> GetAllAsync(InvoiceRequestDTO invoiceRequestDTO);
	Task<InvoiceResponseDTO?> GetAsync(DateTimeOffset? startDate, DateTimeOffset? endDate);

	Task<InvoiceResponseDTO?> GetByIdAsync(Guid id);

	Task<InvoiceResponseDTO?> CreateAsync(CreateInvoiceDTO? dto);

	Task<InvoiceResponseDTO?> UpdateAsync(Guid id, UpdateInvoiceDTO? dto);

	Task SoftDeleteAsync(Guid id);

	Task RecoverAsync(Guid id);

	Task RemoveAsync(Guid id);

	Task ChangeStatusAsync(Guid id);

	Task UpdateInvoiceTotalSumAsync(Guid id);
}
