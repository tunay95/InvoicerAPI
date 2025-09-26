using Invoicer.Business.DTOs.Base;
using Invoicer.Business.DTOs.UserDTOs;

namespace Invoicer.Business.Services.Interfaces;

public interface IUserService
{
	Task<PaginationResponseDTO<UserResponseDTO?>> GetAllAsync(UserRequestDTO userRequestDTO);

	Task<UserResponseDTO?> GetAsync(string? search);

	Task<UserResponseDTO?> GetByIdAsync(Guid id);

	Task<UserResponseDTO?> CreateAsync(CreateUserDTO? dto);

	Task<UserResponseDTO?> UpdateAsync(Guid id, UpdateUserDTO? dto);

	Task ChangePasswordAsync(Guid id, ChangePasswordDTO dto);

	Task SoftDeleteAsync(Guid id);

	Task RecoverAsync(Guid id);

	Task RemoveAsync(Guid id);
}
