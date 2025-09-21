using Invoicer.Business.Services.Interfaces;
using Invoicer.Core.DTOs.UserDTOs;
using Invoicer.Core.Entities;
using InvoicerAPI.Validators.UserDTOValidators;
using Microsoft.AspNetCore.Mvc;

namespace InvoicerAPI.Controllers
{
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
		public async Task<IActionResult> GetAll(string? search)
		{
			var users = await _userService.GetAllAsync(search);

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
		public async Task<IActionResult> Update(Guid id, [FromForm] UpdateUserDTO? updateUserDTO)
		{
			await _userService.UpdateAsync(id, updateUserDTO);

			return Ok("User Successfully Updated !!");
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
}
