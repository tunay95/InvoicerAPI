using FluentValidation;
using Invoicer.Business.DTOs.UserDTOs;

namespace InvoicerAPI.Validators.UserDTOValidators
{
	public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
	{
		public UpdateUserDTOValidator()
		{
			RuleFor(u => u.FirstName)
				.MaximumLength(50).WithMessage("First Name should not be More Than 50 !!");

			RuleFor(u => u.LastName)
				.MaximumLength(75).WithMessage("Last Name should not be More Than 75 !!");

			RuleFor(u => u.Email)
				//.EmailAddress()
				.MaximumLength(50).WithMessage("Email should not be More Than 50 !!")
				.Matches(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]{4,7}+\.[A-Za-z]{2,3}$").WithMessage("Invalid Email !!");

			//RuleFor(u => u.Password)
			//	.MaximumLength(150).WithMessage("Address should not be More Than 150 !!")
			//	.Matches(@"^[A-Z](?=.*\d).{7,}$").WithMessage("Password must start with an uppercase letter, contain at least one digit, and be at least 8 characters long");

			RuleFor(u => u.Address)
				.MaximumLength(150).WithMessage("Address should not be More Than 150 !!");

			RuleFor(u => u.PhoneNumber)
				.Matches(@"^(\+994\d{9}|0\d{9})$").WithMessage("Phone Number must  +994 xxx xx xx (13 chars) or 0xx xxx xx xx (10 chars)")
				.MaximumLength(50).WithMessage("First Name should not be More Than 50 !!");
		}
	}
}
