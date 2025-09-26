using AutoMapper;
using Invoicer.Business.Services.Interfaces;
using Invoicer.Business.DTOs.UserDTOs;
using Invoicer.Core.Entities;
using Invoicer.DAL.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;
using Invoicer.Business.DTOs.Base;

namespace Invoicer.Business.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public UserService(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}



		public async Task<PaginationResponseDTO<UserResponseDTO?>> GetAllAsync(UserRequestDTO dto)
		{
			var users = _userRepository.GetAllAsync(u => !u.IsDeleted);

			if (dto.Search is not null)
				users = users.Where(u => u.FirstName.Contains(dto.Search) ||
										 u.LastName.Contains(dto.Search) ||
										 u.Email.Contains(dto.Search));

			int totalCount = await users.CountAsync();

			if (dto.PageNumber is not null && dto.PageSize is not null)
				users = users
					.Skip((dto.PageNumber.Value - 1) * dto.PageSize.Value)
					.Take(dto.PageSize.Value);


			return new PaginationResponseDTO<UserResponseDTO>
			{
				Entities = _mapper.Map<IEnumerable<UserResponseDTO>>(users),
				TotalCount = totalCount,
				PageNumber = dto.PageNumber ?? 1,
				PageSize = dto.PageSize ?? (totalCount == 0 ? 1 : totalCount)
			}!;
		}


		public async Task<UserResponseDTO?> GetAsync(string? search)
		{
			var users = _userRepository.GetAllAsync(u => u.IsDeleted == false);

			if (search is not null)
				users = users.Where(u => u.FirstName.Contains(search) ||
										 u.LastName.Contains(search) ||
										 u.Email.Contains(search));


			return _mapper.Map<UserResponseDTO>(await users.FirstOrDefaultAsync());
		}


		public async Task<UserResponseDTO?> GetByIdAsync(Guid id)
		{
			var user = await CheckUserIdAsync(id, true);

			return _mapper.Map<UserResponseDTO?>(user);
		}


		public async Task<UserResponseDTO?> CreateAsync(CreateUserDTO? dto)
		{
			if (dto is null)
				throw new ArgumentNullException(nameof(dto));

			var result = await _userRepository.EmailExsist(dto.Email);


			switch (result)
			{
				case null:
					throw new ArgumentNullException(nameof(dto.Email), "There's no Email, You should enter Some Email !!");

				case true:
					throw new InvalidOperationException("This Email is already in Use !!");

				case false:
					var user = _mapper.Map<User>(dto);

					user.CreatedAt = DateTimeOffset.UtcNow;


					await _userRepository.AddAsync(user);

					await _userRepository.CommitAsync();

					return _mapper.Map<UserResponseDTO?>(user);


				default:
					throw new Exception("Some Problems ..");
			}

		}


		public async Task<UserResponseDTO?> UpdateAsync(Guid id, UpdateUserDTO? dto)
		{
			if (dto is null)
				throw new ArgumentNullException(nameof(dto), "The Dto should not be Null !!");

			var previousUser = await CheckUserIdAsync(id);

			var updatedUser = _mapper.Map(dto, previousUser);

			updatedUser.UpdatedAt = DateTimeOffset.UtcNow;

			_userRepository.Update(updatedUser);
			await _userRepository.CommitAsync();

			return _mapper.Map<UserResponseDTO>(updatedUser);
		}


		public async Task SoftDeleteAsync(Guid id)
		{
			var user = await CheckUserIdAsync(id);


			user.IsDeleted = !user.IsDeleted
				? true
				: throw new InvalidOperationException("The User had already been Soft-Deleted !!");


			user.DeletedAt = DateTimeOffset.UtcNow;

			await _userRepository.CommitAsync();
		}


		public async Task RecoverAsync(Guid id)
		{
			var user = await CheckUserIdAsync(id);


			user.IsDeleted = user.IsDeleted
				? false
				: throw new InvalidOperationException("The User had already been Recovered !!");


			user.DeletedAt = null;

			await _userRepository.CommitAsync();
		}


		public async Task RemoveAsync(Guid id)
		{
			var user = await CheckUserIdAsync(id);

			_userRepository.Remove(user);

			await _userRepository.CommitAsync();
		}


		public async Task ChangePasswordAsync(Guid id, ChangePasswordDTO dto)
		{
			if (dto is null)
				throw new ArgumentNullException(nameof(dto));

			var user = await CheckUserIdAsync(id);

			if (dto.PreviousPassword != user.Password)
				throw new InvalidOperationException("The Previous Password is Incorrect !!");

			if (dto.NewPassword == user.Password)
				throw new InvalidOperationException("The New Password cannot be The Same as The Previous Password !!");


			user.Password = dto.NewPassword;

			await _userRepository.CommitAsync();
		}


		private async Task<User> CheckUserIdAsync(Guid id, bool flag = false)
		{
			var user = await _userRepository.GetByIdAsync(id);

			if (user is null || (flag && user.IsDeleted))
				throw new NullReferenceException("The User with This Id Doesn't Exsist !!");


			return user;
		}

	}
}
