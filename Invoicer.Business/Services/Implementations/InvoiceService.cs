using AutoMapper;
using Invoicer.Business.DTOs.Base;
using Invoicer.Business.DTOs.InvoiceDTOs;
using Invoicer.Business.Services.Interfaces;
using Invoicer.Core.Entities;
using Invoicer.Core.Enums;
using Invoicer.DAL.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.Business.Services.Implementations;

public class InvoiceService : IInvoiceService
{
	private readonly IInvoiceRepository _invoiceRepository;
	private readonly IMapper _mapper;

	public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
	{
		_invoiceRepository = invoiceRepository;
		_mapper = mapper;
	}



	public async Task<PaginationResponseDTO<InvoiceResponseDTO?>> GetAllAsync(InvoiceRequestDTO dto)
	{
		var invoices = _invoiceRepository.GetAllAsync(u => u.IsDeleted == false, q => q.Include(i => i.InvoiceRows.Where(r => !r.IsDeleted)));

		if (dto.StartDate is not null)
			invoices = invoices.Where(i => i.StartDate >= dto.StartDate);

		if (dto.EndDate is not null)
			invoices = invoices.Where(i => i.EndDate <= dto.EndDate);

		int totalCount = await invoices.CountAsync();



		if (dto.PageNumber is not null && dto.PageSize is not null)
			invoices = invoices
				.Skip((dto.PageNumber.Value - 1) * dto.PageSize.Value)
				.Take(dto.PageSize.Value);



		return new PaginationResponseDTO<InvoiceResponseDTO>
		{
			Entities = _mapper.Map<IEnumerable<InvoiceResponseDTO>>(invoices),
			TotalCount = totalCount,
			PageNumber = dto.PageNumber ?? 1,
			PageSize = dto.PageSize ?? (totalCount == 0 ? 1 : totalCount)
		}!;
	}


	public async Task<InvoiceResponseDTO?> GetAsync(DateTimeOffset? startDate, DateTimeOffset? endDate)
	{
		var invoices = _invoiceRepository.GetAllAsync(u => !u.IsDeleted, q => q.Include(i => i.InvoiceRows.Where(r => !r.IsDeleted)));

		if (startDate is not null)
			invoices = invoices.Where(i => i.StartDate >= startDate);

		if (endDate is not null)
			invoices = invoices.Where(i => i.EndDate <= endDate);


		return _mapper.Map<InvoiceResponseDTO>(await invoices.FirstOrDefaultAsync());
	}


	public async Task<InvoiceResponseDTO?> GetByIdAsync(Guid id)
	{
		var invoice = await CheckInvoiceIdAsync(id, true);

		return _mapper.Map<InvoiceResponseDTO?>(invoice);
	}


	public async Task<InvoiceResponseDTO?> CreateAsync(CreateInvoiceDTO? dto)
	{
		if (dto is null)
			throw new ArgumentNullException(nameof(dto));

		var invoice = _mapper.Map<Invoice>(dto);

		invoice.CreatedAt = DateTimeOffset.UtcNow;

		invoice.Status = InvoiceStatus.Created;


		await _invoiceRepository.AddAsync(invoice);

		await _invoiceRepository.CommitAsync();

		return _mapper.Map<InvoiceResponseDTO?>(invoice);

	}



	public async Task<InvoiceResponseDTO?> UpdateAsync(Guid id, UpdateInvoiceDTO? dto)
	{
		if (dto is null)
			throw new ArgumentNullException(nameof(dto), "The Dto should not be Null !!");

		var previousInvoice = await CheckInvoiceIdAsync(id);

		if (previousInvoice.Status >= InvoiceStatus.Sent)
			throw new InvalidOperationException("Invoices with Sent Status cannot be Edited.");

		_mapper.Map(dto, previousInvoice);


		previousInvoice.UpdatedAt = DateTimeOffset.UtcNow;


		_invoiceRepository.Update(previousInvoice);

		await _invoiceRepository.CommitAsync();

		return _mapper.Map<InvoiceResponseDTO>(previousInvoice);
	}


	public async Task ChangeStatusAsync(Guid id)
	{
		var invoice = await CheckInvoiceIdAsync(id);

		if (invoice.Status >= InvoiceStatus.Canceled)
			throw new InvalidOperationException("Invoices with Sent Status cannot be Edited.");

		invoice.Status++;


		await _invoiceRepository.CommitAsync();
	}


	public async Task SoftDeleteAsync(Guid id)
	{
		var invoice = await CheckInvoiceIdAsync(id);


		invoice.IsDeleted = !invoice.IsDeleted
			? true
			: throw new InvalidOperationException("The Invoice had already been Soft-Deleted !!");

		invoice.DeletedAt = DateTimeOffset.UtcNow;


		await _invoiceRepository.CommitAsync();
	}


	public async Task RecoverAsync(Guid id)
	{
		var invoice = await CheckInvoiceIdAsync(id);


		invoice.IsDeleted = invoice.IsDeleted
			? false
			: throw new InvalidOperationException("The Invoice had already been Recovered !!");


		invoice.DeletedAt = null;

		await _invoiceRepository.CommitAsync();
	}


	public async Task RemoveAsync(Guid id)
	{
		var invoice = await CheckInvoiceIdAsync(id);

		if (invoice.Status >= InvoiceStatus.Sent)
			throw new InvalidOperationException("Invoices with Sent Status cannot be Remove.");


		_invoiceRepository.Remove(invoice);

		await _invoiceRepository.CommitAsync();
	}



	private async Task<Invoice> CheckInvoiceIdAsync(Guid id, bool flag = false)
	{
		var invoice = await _invoiceRepository.GetByIdAsync(id);

		if (invoice is null || (flag && invoice.IsDeleted))
			throw new NullReferenceException("The Invoice with This Id Doesn't Exsist !!");


		return invoice;
	}


	public async Task UpdateInvoiceTotalSumAsync(Guid id)
	{
		var updatedInvoice = await _invoiceRepository.Table
			.Include(i => i.InvoiceRows)
			.FirstOrDefaultAsync(i => i.Id == id);

		if (updatedInvoice is null)
			throw new NullReferenceException("Invoice not Found !!");

		updatedInvoice!.TotalSum = updatedInvoice.InvoiceRows
			.Where(ir => !ir.IsDeleted)
			.Sum(ir => ir.Sum);

		await _invoiceRepository.CommitAsync();
	}

}
