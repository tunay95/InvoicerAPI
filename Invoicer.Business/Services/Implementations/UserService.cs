using AutoMapper;
using Invoicer.Business.Services.Interfaces;
using Invoicer.Core.DTOs.UserDTOs;
using Invoicer.Core.Entities;
using Invoicer.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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



		public async Task<IEnumerable<UserResponseDTO?>> GetAllAsync(string? search)
		{
			var users = _userRepository.GetAllAsync(u => u.IsDeleted == false);

			if (search is not null)
				users = users.Where(u => u.FirstName.Contains(search) ||
										 u.LastName.Contains(search) ||
										 u.Email.Contains(search));


			return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
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
			if (dto is null) throw new ArgumentNullException(nameof(dto));

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

			//_userRepository.Update(updatedUser);
			await _userRepository.CommitAsync();

			return _mapper.Map<UserResponseDTO>(updatedUser);
		}


		public async Task SoftDeleteAsync(Guid id)
		{
			var user = await CheckUserIdAsync(id);


			user.IsDeleted = !user.IsDeleted
				? true
				: throw new InvalidOperationException("The User had already been Soft-Deleted !!");


			await _userRepository.CommitAsync();
		}


		public async Task RecoverAsync(Guid id)
		{
			var user = await CheckUserIdAsync(id);


			user.IsDeleted = user.IsDeleted
				? false
				: throw new InvalidOperationException("The User had already been Recovered !!");


			await _userRepository.CommitAsync();
		}


		public async Task RemoveAsync(Guid id)
		{
			var user = await CheckUserIdAsync(id);



			_userRepository.Remove(user);

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
