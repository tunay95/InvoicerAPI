using Invoicer.Business.DTOs.Base;
using Invoicer.Business.DTOs.CustomerDTOs;

namespace Invoicer.Business.Services.Interfaces;

public interface ICustomerService
{
	Task<PaginationResponseDTO<CustomerResponseDTO?>> GetAllAsync(CustomerRequestDTO customerRequestDTO);

	Task<CustomerResponseDTO?> GetAsync(string? search);

	Task<CustomerResponseDTO?> GetByIdAsync(Guid id);

	Task<CustomerResponseDTO?> CreateAsync(CreateCustomerDTO? dto);

	Task<CustomerResponseDTO?> UpdateAsync(Guid id, UpdateCustomerDTO? updateCustomerDTO);

	Task SoftDeleteAsync(Guid id);

	Task RecoverAsync(Guid id);

	Task RemoveAsync(Guid id);
}
