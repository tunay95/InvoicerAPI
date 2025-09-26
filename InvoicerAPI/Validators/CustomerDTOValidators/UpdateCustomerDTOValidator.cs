using FluentValidation;
using Invoicer.Business.DTOs.CustomerDTOs;

namespace InvoicerAPI.Validators.CustomerDTOValidators;

public class UpdateCustomerDTOValidator : AbstractValidator<UpdateCustomerDTO>
{
	public UpdateCustomerDTOValidator()
	{

		RuleFor(u => u.Name)
			.MinimumLength(2).WithMessage("First Name should not be Less Than 2 !!")
			.MaximumLength(50).WithMessage("First Name should not be More Than 50 !!");


		RuleFor(u => u.Email)
			//.EmailAddress()
			.MinimumLength(12).WithMessage("Email should not be Less Than 12 !!  =>	 ...@gmail.com")
			.MaximumLength(50).WithMessage("Email should not be More Than 50 !!  =>  ...@gmail.com")
			.Matches(@"^[A-Za-z0-9._%+-]{1,64}@[A-Za-z0-9.-]{4,7}\.[A-Za-z]{2,3}$").WithMessage("Invalid Email !!");



		RuleFor(u => u.PhoneNumber)
			.Matches(@"^(\+994\d{9}|0\d{9})$").WithMessage("Phone Number must  +994 xxx xx xx (13 chars) or 0xx xxx xx xx (10 chars)")
			.MaximumLength(50).WithMessage("First Name should not be More Than 50 !!");

	}
}
