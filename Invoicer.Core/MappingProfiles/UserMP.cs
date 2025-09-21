using AutoMapper;
using Invoicer.Core.DTOs.UserDTOs;
using Invoicer.Core.Entities;

namespace Invoicer.Core.MappingProfiles;

public class UserMP : Profile
{
	public UserMP()
	{
		CreateMap<CreateUserDTO, User>().ReverseMap();
		CreateMap<UpdateUserDTO, User>().ReverseMap();
		CreateMap<User, UserResponseDTO>();
	}
}
