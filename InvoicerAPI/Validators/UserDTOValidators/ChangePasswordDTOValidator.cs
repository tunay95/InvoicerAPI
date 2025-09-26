using FluentValidation;
using Invoicer.Business.DTOs.UserDTOs;

namespace InvoicerAPI.Validators.UserDTOValidators;

public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
{
	public ChangePasswordDTOValidator()
	{
		RuleFor(u => u.PreviousPassword)
			.MaximumLength(150).WithMessage("Address should not be More Than 150 !!")
			.Matches(@"^[A-Z](?=.*\d).{7,}$").WithMessage("Password must start with an uppercase letter, contain at least one digit, and be at least 8 characters long");

		RuleFor(u => u.NewPassword)
			.MaximumLength(150).WithMessage("Address should not be More Than 150 !!")
			.Matches(@"^[A-Z](?=.*\d).{7,}$").WithMessage("Password must start with an uppercase letter, contain at least one digit, and be at least 8 characters long");
	}
}
