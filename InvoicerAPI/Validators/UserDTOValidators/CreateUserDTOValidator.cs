using FluentValidation;
using Invoicer.Business.DTOs.UserDTOs;

namespace InvoicerAPI.Validators.UserDTOValidators;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
	public CreateUserDTOValidator()
	{
		RuleFor(u => u.FirstName)
			.NotEmpty().WithMessage("First Name is Required !!")
			.NotNull().WithMessage("First Name is Required !!")
			.MinimumLength(2).WithMessage("First Name should not be Less Than 2 !!")
			.MaximumLength(50).WithMessage("First Name should not be More Than 50 !!");

		RuleFor(u => u.LastName)
			.MinimumLength(2).WithMessage("First Name should not be Less Than 2 !!")
			.MaximumLength(75).WithMessage("Last Name should not be More Than 75 !!");

		RuleFor(u => u.Email)
			//.EmailAddress()
			.NotEmpty().WithMessage("Email is Required !!")
			.NotNull().WithMessage("Email is Required !!")
			.MinimumLength(12).WithMessage("Email should not be Less Than 12 !!  =>	 ...@gmail.com")
			.MaximumLength(50).WithMessage("Email should not be More Than 50 !!  =>  ...@gmail.com")
			.Matches(@"^[A-Za-z0-9._%+-]{1,64}@[A-Za-z0-9.-]{4,7}\.[A-Za-z]{2,3}$").WithMessage("Invalid Email !!");


		RuleFor(u => u.Password)
			.NotEmpty().WithMessage("Password is Required !!")
			.NotNull().WithMessage("Password is Required !!")
			.MinimumLength(8).WithMessage("Password should be At Least 8 Characters Long !!")
			.MaximumLength(150).WithMessage("Address should not be More Than 150 !!")
			.Matches(@"^[A-Z](?=.*\d).{7,}$").WithMessage("Password must start with an uppercase letter, contain at least one digit, and be at least 8 characters long");

		RuleFor(u => u.Address)
			.MinimumLength(3).WithMessage("Address should be At Least 3 Characters Long !!")
			.MaximumLength(150).WithMessage("Address should not be More Than 150 !!");

		RuleFor(u => u.PhoneNumber)
			.Matches(@"^(\+994\d{9}|0\d{9})$").WithMessage("Phone Number must  +994 xxx xx xx (13 chars) or 0xx xxx xx xx (10 chars)")
			.MaximumLength(50).WithMessage("First Name should not be More Than 50 !!");
	}
}
