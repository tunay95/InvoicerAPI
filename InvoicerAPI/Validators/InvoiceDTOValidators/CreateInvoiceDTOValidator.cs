using FluentValidation;
using Invoicer.Business.DTOs.InvoiceDTOs;

namespace InvoicerAPI.Validators.InvoiceDTOValidators;

public class CreateInvoiceDTOValidator : AbstractValidator<CreateInvoiceDTO>
{
	public CreateInvoiceDTOValidator()
	{

		RuleFor(i => i.StartDate)
			 .NotNull().WithMessage("StartDate cannot be null.")
			 .NotEmpty().WithMessage("StartDate cannot be empty.");

		RuleFor(i => i.EndDate)
			.NotNull().WithMessage("EndDate cannot be null.")
			.NotEmpty().WithMessage("EndDate cannot be empty.")
			.GreaterThan(x => x.StartDate)
			.WithMessage("EndDate must be greater than StartDate.");

		RuleFor(i => i.Comment)
			.MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");

		RuleFor(i => i.CustomerId)
			.NotNull().WithMessage("UserId cannot be null.")
			.NotEmpty().WithMessage("UserId cannot be empty.");

	}
}
