using FluentValidation;
using Invoicer.Core.DTOs.UserDTOs;

namespace InvoicerAPI.Validators.UserDTOValidators
{
	public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
	{
		public CreateUserDTOValidator()
		{

			RuleFor(u => u.FirstName)
				.NotEmpty().WithMessage("First Name is Required !!")
				.MaximumLength(50).WithMessage("First Name should not be More Than 50 !!");

			RuleFor(u => u.LastName)
				.NotEmpty().WithMessage("Last Name is Required !!")
				.MaximumLength(75).WithMessage("Last Name should not be More Than 75 !!");

			RuleFor(u => u.Email)
				.NotEmpty().WithMessage("Email is Required !!")
				.MaximumLength(50).WithMessage("Email should not be More Than 50 !!")
				.Matches(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,3}$").WithMessage("Invalid Email !!");

			RuleFor(u => u.Password)
				.NotEmpty().WithMessage("Password is Required !!")
				.MaximumLength(150).WithMessage("Address should not be More Than 150 !!")
				.Matches(@"^[A-Z](?=.*\d).{7,}$").WithMessage("Password must start with an uppercase letter, contain at least one digit, and be at least 8 characters long");

			RuleFor(u => u.Address)
				.NotEmpty().WithMessage("Address is Required !!")
				.MaximumLength(150).WithMessage("Address should not be More Than 150 !!");

			RuleFor(u => u.PhoneNumber)
				.Matches(@"^(\+994\d{9}|0\d{9})$").WithMessage("Phone Number must  +994 xxx xx xx (13 chars) or 0xx xxx xx xx (10 chars)")
				.NotEmpty().WithMessage("Phone Number is Required !!")
				.MaximumLength(50).WithMessage("First Name should not be More Than 50 !!");

		}
	}
}
