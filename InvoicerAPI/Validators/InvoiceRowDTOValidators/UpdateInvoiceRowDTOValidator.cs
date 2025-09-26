using FluentValidation;
using Invoicer.Business.DTOs.InvoiceRowDTOs;

namespace InvoicerAPI.Validators.InvoiceRowDTOValidators;

public class UpdateInvoiceRowDTOValidator : AbstractValidator<UpdateInvoiceRowDTO>
{
	public UpdateInvoiceRowDTOValidator()
	{

		RuleFor(x => x.Service)
			.MaximumLength(75).WithMessage("Service cannot exceed 75 characters.");

		RuleFor(x => x.Quantity)
			.GreaterThan(0).WithMessage("Quantity must be greater than 0.");

		RuleFor(x => x.Amount)
			.GreaterThan(0).WithMessage("Amount must be greater than 0.");

	}
}
