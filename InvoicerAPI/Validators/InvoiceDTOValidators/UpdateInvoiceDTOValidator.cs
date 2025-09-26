using FluentValidation;
using Invoicer.Business.DTOs.InvoiceDTOs;

namespace InvoicerAPI.Validators.InvoiceDTOValidators;

public class UpdateInvoiceDTOValidator : AbstractValidator<UpdateInvoiceDTO>
{
	public UpdateInvoiceDTOValidator()
	{

		RuleFor(i => i.EndDate)
			.GreaterThan(x => x.StartDate)
			.WithMessage("EndDate must be greater than StartDate.");

		RuleFor(i => i.Comment)
			.MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");

	}
}
