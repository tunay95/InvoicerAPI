using Invoicer.Business.DTOs.InvoiceDTOs;
using Invoicer.Business.Services.Interfaces;
using InvoicerAPI.Validators.InvoiceDTOValidators;
using Microsoft.AspNetCore.Mvc;

namespace InvoicerAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoicesController : ControllerBase
	{
		private readonly IInvoiceService _invoiceService;

		public InvoicesController(IInvoiceService invoiceService)
		{
			_invoiceService = invoiceService;
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> GetAll([FromQuery] InvoiceRequestDTO invoiceRequestDTO)
		{
			var invoices = await _invoiceService.GetAllAsync(invoiceRequestDTO);

			return Ok(invoices);
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> Get(DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			var invoices = await _invoiceService.GetAsync(startDate, endDate);

			return Ok(invoices);
		}


		[HttpGet("[action]/{id:guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var invoices = await _invoiceService.GetByIdAsync(id);

			return Ok(invoices);
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> Create([FromBody] CreateInvoiceDTO createInvoiceDTO)
		{
			var validateResult = await new CreateInvoiceDTOValidator().ValidateAsync(createInvoiceDTO);


			if (!validateResult.IsValid)
			{
				var errors = validateResult.Errors
					.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

				return BadRequest(errors);
			}

			var invoice = await _invoiceService.CreateAsync(createInvoiceDTO);

			return Created("Invoice Successfully Created !!", invoice);
		}


		[HttpPut("[action]/{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInvoiceDTO? updateInvoiceDTO)
		{
			var validateResult = await new UpdateInvoiceDTOValidator().ValidateAsync(updateInvoiceDTO!);


			if (!validateResult.IsValid)
			{
				var errors = validateResult.Errors
					.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

				return BadRequest(errors);
			}

			await _invoiceService.UpdateAsync(id, updateInvoiceDTO);

			return Ok("Invoice Successfully Updated !!");
		}


		[HttpPatch("[action]/{id:guid}")]
		public async Task<IActionResult> ChangeStatusAsync(Guid id)
		{
			await _invoiceService.ChangeStatusAsync(id);

			return Ok(new { message = "Invoice Status Successfully changed !!" });
		}


		[HttpPatch("[action]/{id:guid}")]
		public async Task<IActionResult> SoftDelete(Guid id)
		{
			await _invoiceService.SoftDeleteAsync(id);

			return Ok(new { message = "Invoice Successfully Soft-Deleted !!" });
		}


		[HttpPatch("[action]/{id:guid}")]
		public async Task<IActionResult> Recover(Guid id)
		{
			await _invoiceService.RecoverAsync(id);

			return Ok(new { message = "Invoice Successfully Recovered !!" });
		}


		[HttpDelete("[action]/{id:guid}")]
		public async Task<IActionResult> Remove(Guid id)
		{
			await _invoiceService.RemoveAsync(id);

			return Ok(new { message = "Invoice Successfully Removed !!" });
		}

	}
}
