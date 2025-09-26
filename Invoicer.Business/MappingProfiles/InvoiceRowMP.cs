using AutoMapper;
using Invoicer.Business.DTOs.InvoiceRowDTOs;
using Invoicer.Core.Entities;

namespace Invoicer.Business.MappingProfiles;

public class InvoiceRowMP : Profile
{
	public InvoiceRowMP()
	{
		CreateMap<InvoiceRow, InvoiceRowResponseDTO>();
		CreateMap<CreateInvoiceRowDTO, InvoiceRow>().ReverseMap();
		CreateMap<UpdateInvoiceRowDTO, InvoiceRow>()
				.ForAllMembers(opt =>
				opt.Condition(
					(src, DestinationMemberNamingConvention, srcMember) => srcMember != null)
				);
	}

}
