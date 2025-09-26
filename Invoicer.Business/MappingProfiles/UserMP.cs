using AutoMapper;
using Invoicer.Business.DTOs.UserDTOs;
using Invoicer.Core.Entities;

namespace Invoicer.Core.MappingProfiles;

public class UserMP : Profile
{
	public UserMP()
	{
		CreateMap<User, UserResponseDTO>();
		CreateMap<CreateUserDTO, User>().ReverseMap();
		CreateMap<UpdateUserDTO, User>()
			.ForAllMembers(opt =>
			opt.Condition(
				(src, DestinationMemberNamingConvention, srcMember) => srcMember != null)
			);
	}
}
