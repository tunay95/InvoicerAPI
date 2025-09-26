using Invoicer.Business.DTOs.CustomerDTOs;
using Invoicer.Business.Services.Interfaces;
using Invoicer.Core.Entities;
using InvoicerAPI.Validators.CustomerDTOValidators;
using Microsoft.AspNetCore.Mvc;

namespace InvoicerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
	private readonly ICustomerService _customerService;

	public CustomersController(ICustomerService customerService)
	{
		_customerService = customerService;
	}


	[HttpGet("[action]")]
	public async Task<IActionResult> GetAll([FromQuery] CustomerRequestDTO customerRequestDTO)
	{
		var customers = await _customerService.GetAllAsync(customerRequestDTO);

		return Ok(customers);
	}


	[HttpGet("[action]")]
	public async Task<IActionResult> Get(string? search)
	{
		var customers = await _customerService.GetAsync(search);

		return Ok(customers);
	}


	[HttpGet("[action]/{id:guid}")]
	public async Task<IActionResult> GetById([FromRoute] Guid id)
	{
		var customers = await _customerService.GetByIdAsync(id);

		return Ok(customers);
	}


	[HttpPost("[action]")]
	public async Task<IActionResult> Create([FromBody] CreateCustomerDTO createCustomerDTO)
	{
		var validateResult = await new CreateCustomerDTOValidator().ValidateAsync(createCustomerDTO);


		if (!validateResult.IsValid)
		{
			var errors = validateResult.Errors
				.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

			return BadRequest(errors);
		}

		var customer = await _customerService.CreateAsync(createCustomerDTO);

		return Created("", new { message = "Customer Successfully Created!!", data = customer });
	}


	[HttpPut("[action]/{id:guid}")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDTO? updateCustomerDTO)
	{
		var validateResult = await new UpdateCustomerDTOValidator().ValidateAsync(updateCustomerDTO!);


		if (!validateResult.IsValid)
		{
			var errors = validateResult.Errors
				.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

			return BadRequest(errors);
		}

		await _customerService.UpdateAsync(id, updateCustomerDTO);

		return Ok("Customer Successfully Updated !!");
	}


	[HttpPatch("[action]/{id:guid}")]
	public async Task<IActionResult> SoftDelete(Guid id)
	{
		await _customerService.SoftDeleteAsync(id);

		return Ok(new { message = "Customer Successfully Soft-Deleted !!" });
	}


	[HttpPatch("[action]/{id:guid}")]
	public async Task<IActionResult> Recover(Guid id)
	{
		await _customerService.RecoverAsync(id);

		return Ok(new { message = "Customer Successfully Recovered !!" });
	}


	[HttpDelete("[action]/{id:guid}")]
	public async Task<IActionResult> Remove(Guid id)
	{
		await _customerService.RemoveAsync(id);

		return Ok(new { message = "Customer Successfully Removed !!" });
	}
}
