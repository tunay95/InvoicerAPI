using AutoMapper;
using Invoicer.Business.DTOs.InvoiceDTOs;
using Invoicer.Core.Entities;


namespace Invoicer.Business.MappingProfiles;

public class InvoiceMP : Profile
{
	public InvoiceMP()
	{
		CreateMap<Invoice, InvoiceResponseDTO>();
		CreateMap<CreateInvoiceDTO, Invoice>().ReverseMap();
		CreateMap<UpdateInvoiceDTO, Invoice>()
				.ForAllMembers(opts =>
				 opts.Condition((src, dest, srcMember) =>
			 srcMember != null));


		//opt.Condition((src, dest, srcMember, destMember, context) => {
		//	if (srcMember == null) return false; // reference type null
		//										 // nullable value type üçün
		//	var type = srcMember.GetType();
		//	if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
		//	{
		//		var hasValueProp = type.GetProperty("HasValue");
		//		if (hasValueProp != null)
		//			return (bool)hasValueProp.GetValue(srcMember)!;
		//	}
		//	return true;
	}
}
