using AutoMapper;
using Invoicer.Business.DTOs.CustomerDTOs;
using Invoicer.Core.Entities;

namespace Invoicer.Business.MappingProfiles;

public class CustomerMP : Profile
{
    public CustomerMP()
    {
		CreateMap<Customer, CustomerResponseDTO>();
		CreateMap<CreateCustomerDTO, Customer>().ReverseMap();
		CreateMap<UpdateCustomerDTO, Customer>()
			.ForAllMembers(opt =>
			opt.Condition(
				(src, DestinationMemberNamingConvention, srcMember) => srcMember != null)
			);
	}
}
