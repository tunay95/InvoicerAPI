using Invoicer.Business.DTOs.InvoiceRowDTOs;
using Invoicer.Business.Services.Interfaces;
using InvoicerAPI.Validators.InvoiceRowDTOValidators;
using Microsoft.AspNetCore.Mvc;

namespace InvoicerAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoiceRowsController : ControllerBase
	{
		private readonly IInvoiceRowService _invoiceRowService;

		public InvoiceRowsController(IInvoiceRowService invoiceRowService)
		{
			_invoiceRowService = invoiceRowService;
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> GetAll([FromQuery] InvoiceRowRequestDTO invoiceRowRequestDTO)
		{
			var invoiceRows = await _invoiceRowService.GetAllAsync(invoiceRowRequestDTO);

			return Ok(invoiceRows);
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> Get([FromQuery] string? search = null, [FromQuery] decimal? minSum = null, [FromQuery] decimal? maxSum = null)
		{
			var invoiceRow = await _invoiceRowService.GetAsync(search, minSum, maxSum);

			return Ok(invoiceRow);
		}


		[HttpGet("[action]/{id:guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var invoiceRow = await _invoiceRowService.GetByIdAsync(id);

			return Ok(invoiceRow);
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> Create([FromBody] CreateInvoiceRowDTO createInvoiceRowDTO)
		{
			var validateResult = await new CreateInvoiceRowDTOValidator().ValidateAsync(createInvoiceRowDTO);


			if (!validateResult.IsValid)
			{
				var errors = validateResult.Errors
					.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

				return BadRequest(errors);
			}

			var invoiceRow = await _invoiceRowService.CreateAsync(createInvoiceRowDTO);

			return Created("Invoice Row Successfully Created !!", invoiceRow);
		}


		[HttpPut("[action]/{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInvoiceRowDTO? updateInvoiceRowDTO)
		{
			var validateResult = await new UpdateInvoiceRowDTOValidator().ValidateAsync(updateInvoiceRowDTO!);


			if (!validateResult.IsValid)
			{
				var errors = validateResult.Errors
					.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

				return BadRequest(errors);
			}

			await _invoiceRowService.UpdateAsync(id, updateInvoiceRowDTO);

			return Ok("Invoice Row Successfully Updated !!");
		}


		[HttpPatch("[action]/{id:guid}")]
		public async Task<IActionResult> SoftDelete(Guid id)
		{
			await _invoiceRowService.SoftDeleteAsync(id);

			return Ok(new { message = "Invoice Row Successfully Soft-Deleted !!" });
		}


		[HttpPatch("[action]/{id:guid}")]
		public async Task<IActionResult> Recover(Guid id)
		{
			await _invoiceRowService.RecoverAsync(id);

			return Ok(new { message = "Invoice Row Successfully Recovered !!" });
		}


		[HttpDelete("[action]/{id:guid}")]
		public async Task<IActionResult> Remove(Guid id)
		{
			await _invoiceRowService.RemoveAsync(id);

			return Ok(new { message = "Invoice Row Successfully Removed !!" });
		}
	}
}
