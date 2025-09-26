using Invoicer.Business.DTOs.Base;
using Invoicer.Business.DTOs.InvoiceDTOs;
using Invoicer.Business.DTOs.InvoiceRowDTOs;

namespace Invoicer.Business.Services.Interfaces;

public interface IInvoiceRowService
{
	Task<PaginationResponseDTO<InvoiceRowResponseDTO?>> GetAllAsync(InvoiceRowRequestDTO invoiceRowRequestDTO);
	Task<InvoiceRowResponseDTO?> GetAsync(string? search, decimal? minSum, decimal? maxSum);

	Task<InvoiceRowResponseDTO?> GetByIdAsync(Guid id);

	Task<InvoiceRowResponseDTO?> CreateAsync(CreateInvoiceRowDTO? dto);

	Task<InvoiceRowResponseDTO?> UpdateAsync(Guid id, UpdateInvoiceRowDTO? dto);

	Task SoftDeleteAsync(Guid id);

	Task RecoverAsync(Guid id);

	Task RemoveAsync(Guid id);
}
