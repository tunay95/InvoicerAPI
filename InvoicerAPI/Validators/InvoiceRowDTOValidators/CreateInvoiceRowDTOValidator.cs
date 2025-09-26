using FluentValidation;
using Invoicer.Business.DTOs.InvoiceRowDTOs;

namespace InvoicerAPI.Validators.InvoiceRowDTOValidators;

public class CreateInvoiceRowDTOValidator:AbstractValidator<CreateInvoiceRowDTO>
{
    public CreateInvoiceRowDTOValidator()
    {
		RuleFor(x => x.Service)
			.NotNull().WithMessage("Service cannot be null.")
			.NotEmpty().WithMessage("Service cannot be empty.")
			.MaximumLength(200).WithMessage("Service cannot exceed 200 characters.");

		RuleFor(x => x.Quantity)
			.NotNull().WithMessage("Quantity cannot be null.")
			.GreaterThan(0).WithMessage("Quantity must be greater than 0.");

		RuleFor(x => x.Amount)
			.NotNull().WithMessage("Amount cannot be null.")
			.GreaterThan(0).WithMessage("Amount must be greater than 0.");


		RuleFor(x => x.InvoiceId)
			.NotNull().WithMessage("InvoiceId cannot be null.")
			.NotEmpty().WithMessage("InvoiceId cannot be empty.");
	}
}
