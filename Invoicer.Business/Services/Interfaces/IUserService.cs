using Invoicer.Core.DTOs.UserDTOs;

namespace Invoicer.Business.Services.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<UserResponseDTO?>> GetAllAsync(string? search);
		Task<UserResponseDTO?> GetAsync(string? search);

		Task<UserResponseDTO?> GetByIdAsync(Guid id);

		Task<UserResponseDTO?> CreateAsync(CreateUserDTO? dto);

		Task<UserResponseDTO?> UpdateAsync(Guid id, UpdateUserDTO? dto);

		Task SoftDeleteAsync(Guid id);

		Task RecoverAsync(Guid id);

		Task RemoveAsync(Guid id);
	}
}
