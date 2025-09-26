using Invoicer.Business.Services.Interfaces;
using Invoicer.Business.DTOs.UserDTOs;
using InvoicerAPI.Validators.UserDTOValidators;
using Microsoft.AspNetCore.Mvc;
using Invoicer.Business.DTOs.CustomerDTOs;
using InvoicerAPI.Validators.CustomerDTOValidators;

namespace InvoicerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
	private readonly IUserService _userService;

	public UsersController(IUserService userService)
	{
		_userService = userService;
	}


	[HttpGet("[action]")]
	public async Task<IActionResult> GetAll([FromQuery] UserRequestDTO userRequestDTO)
	{
		var users = await _userService.GetAllAsync(userRequestDTO);

		return Ok(users);
	}


	[HttpGet("[action]")]
	public async Task<IActionResult> Get(string? search)
	{
		var users = await _userService.GetAsync(search);

		return Ok(users);
	}


	[HttpGet("[action]/{id:guid}")]
	public async Task<IActionResult> GetById([FromRoute] Guid id)
	{
		var users = await _userService.GetByIdAsync(id);

		return Ok(users);
	}


	[HttpPost("[action]")]
	public async Task<IActionResult> Create([FromBody] CreateUserDTO createUserDTO)
	{
		var validateResult = await new CreateUserDTOValidator().ValidateAsync(createUserDTO);


		if (!validateResult.IsValid)
		{
			var errors = validateResult.Errors
				.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

			return BadRequest(errors);
		}

		var user = await _userService.CreateAsync(createUserDTO);

		return Created("User Successfully Created !!", user);
	}


	[HttpPut("[action]/{id:guid}")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDTO? updateUserDTO)
	{
		var validateResult = await new UpdateUserDTOValidator().ValidateAsync(updateUserDTO!);

		if (!validateResult.IsValid)
		{
			var errors = validateResult.Errors
				.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

			return BadRequest(errors);
		}

		await _userService.UpdateAsync(id, updateUserDTO);

		return Ok("User Successfully Updated !!");
	}


	[HttpPatch("[action]/{id:guid}")]
	public async Task<IActionResult> ChangePasswordAsync(Guid id, ChangePasswordDTO changePasswordDTO)
	{
		var validateResult = await new ChangePasswordDTOValidator().ValidateAsync(changePasswordDTO!);

		if (!validateResult.IsValid)
		{
			var errors = validateResult.Errors
				.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

			return BadRequest(errors);
		}

		await _userService.ChangePasswordAsync(id, changePasswordDTO);

		return Ok(new { message = "User Password Successfully Changed !!" });
	}


	[HttpPatch("[action]/{id:guid}")]
	public async Task<IActionResult> SoftDelete(Guid id)
	{
		await _userService.SoftDeleteAsync(id);

		return Ok(new { message = "User Successfully Soft-Deleted !!" });
	}


	[HttpPatch("[action]/{id:guid}")]
	public async Task<IActionResult> Recover(Guid id)
	{
		await _userService.RecoverAsync(id);

		return Ok(new { message = "User Successfully Recovered !!" });
	}


	[HttpDelete("[action]/{id:guid}")]
	public async Task<IActionResult> Remove(Guid id)
	{
		await _userService.RemoveAsync(id);

		return Ok(new { message = "User Successfully Removed !!" });
	}
}
