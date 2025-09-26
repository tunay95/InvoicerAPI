using AutoMapper;
using Invoicer.Business.DTOs.Base;
using Invoicer.Business.DTOs.InvoiceRowDTOs;
using Invoicer.Business.Services.Interfaces;
using Invoicer.Core.Entities;
using Invoicer.DAL.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.Business.Services.Implementations;

public class InvoiceRowService : IInvoiceRowService
{
	private readonly IInvoiceRowRepository _invoiceRowRepository;
	private readonly IInvoiceService _invoiceService;
	private readonly IMapper _mapper;

	public InvoiceRowService(IInvoiceService invoiceService, IInvoiceRowRepository invoiceRowRowRepository, IMapper mapper)
	{
		_invoiceService = invoiceService;
		_invoiceRowRepository = invoiceRowRowRepository;
		_mapper = mapper;
	}



	public async Task<PaginationResponseDTO<InvoiceRowResponseDTO?>> GetAllAsync(InvoiceRowRequestDTO dto)
	{
		var invoiceRows = _invoiceRowRepository.GetAllAsync(u => !u.IsDeleted);

		if (dto.Search is not null)
			invoiceRows = invoiceRows.Where(ir => ir.Service.Contains(dto.Search));

		if (dto.MinSum is not null)
			invoiceRows = invoiceRows.Where(ir => ir.Sum >= dto.MinSum);

		if (dto.MaxSum is not null)
			invoiceRows = invoiceRows.Where(ir => ir.Sum <= dto.MaxSum);


		int totalCount = await invoiceRows.CountAsync();

		if (dto.PageNumber is not null && dto.PageSize is not null)
			invoiceRows = invoiceRows
				.Skip((dto.PageNumber.Value - 1) * dto.PageSize.Value)
				.Take(dto.PageSize.Value);


		return new PaginationResponseDTO<InvoiceRowResponseDTO>
		{
			Entities = _mapper.Map<IEnumerable<InvoiceRowResponseDTO>>(invoiceRows),
			TotalCount = totalCount,
			PageNumber = dto.PageNumber ?? 1,
			PageSize = dto.PageSize ?? (totalCount == 0 ? 1 : totalCount)
		}!;
	}


	public async Task<InvoiceRowResponseDTO?> GetAsync(string? search, decimal? minSum, decimal? maxSum)
	{
		var invoiceRows = _invoiceRowRepository.GetAllAsync(u => !u.IsDeleted);

		if (search is not null)
			invoiceRows = invoiceRows.Where(ir => ir.Service.Contains(search));

		if (minSum is not null)
			invoiceRows = invoiceRows.Where(ir => ir.Sum >= minSum);

		if (maxSum is not null)
			invoiceRows = invoiceRows.Where(ir => ir.Sum <= maxSum);


		return _mapper.Map<InvoiceRowResponseDTO>(await invoiceRows.FirstOrDefaultAsync());
	}


	public async Task<InvoiceRowResponseDTO?> GetByIdAsync(Guid id)
	{
		var invoiceRow = await CheckInvoiceRowIdAsync(id, true);

		return _mapper.Map<InvoiceRowResponseDTO?>(invoiceRow);
	}


	public async Task<InvoiceRowResponseDTO?> CreateAsync(CreateInvoiceRowDTO? dto)
	{
		if (dto is null)
			throw new ArgumentNullException(nameof(dto));

		var invoiceRow = _mapper.Map<InvoiceRow>(dto);

		invoiceRow.CreatedAt = DateTimeOffset.UtcNow;

		invoiceRow.Sum = invoiceRow.Amount * invoiceRow.Quantity;


		await _invoiceRowRepository.AddAsync(invoiceRow);

		await _invoiceRowRepository.CommitAsync();

		await _invoiceService.UpdateInvoiceTotalSumAsync(dto.InvoiceId);

		return _mapper.Map<InvoiceRowResponseDTO?>(invoiceRow);

	}


	public async Task<InvoiceRowResponseDTO?> UpdateAsync(Guid id, UpdateInvoiceRowDTO? dto)
	{
		if (dto is null)
			throw new ArgumentNullException(nameof(dto), "The Dto should not be Null !!");

		var previousInvoiceRow = await CheckInvoiceRowIdAsync(id);
		_mapper.Map(dto, previousInvoiceRow);

		previousInvoiceRow.UpdatedAt = DateTimeOffset.UtcNow;

		previousInvoiceRow.Sum = previousInvoiceRow.Amount * previousInvoiceRow.Quantity;

		_invoiceRowRepository.Update(previousInvoiceRow);

		await _invoiceRowRepository.CommitAsync();

		await _invoiceService.UpdateInvoiceTotalSumAsync(dto.InvoiceId);


		return _mapper.Map<InvoiceRowResponseDTO>(previousInvoiceRow);
	}


	public async Task SoftDeleteAsync(Guid id)
	{
		var invoiceRow = await CheckInvoiceRowIdAsync(id);


		invoiceRow.IsDeleted = !invoiceRow.IsDeleted
			? true
			: throw new InvalidOperationException("The Invoice had already been Soft-Deleted !!");


		invoiceRow.DeletedAt = DateTimeOffset.UtcNow;

		await _invoiceRowRepository.CommitAsync();

		await _invoiceService.UpdateInvoiceTotalSumAsync(invoiceRow.InvoiceId);
	}


	public async Task RecoverAsync(Guid id)
	{
		var invoiceRow = await CheckInvoiceRowIdAsync(id);


		invoiceRow.IsDeleted = invoiceRow.IsDeleted
			? false
			: throw new InvalidOperationException("The Invoice had already been Recovered !!");


		invoiceRow.DeletedAt = null;

		await _invoiceRowRepository.CommitAsync();

		await _invoiceService.UpdateInvoiceTotalSumAsync(invoiceRow.InvoiceId);
	}


	public async Task RemoveAsync(Guid id)
	{
		var invoiceRow = await CheckInvoiceRowIdAsync(id);

		_invoiceRowRepository.Remove(invoiceRow);

		await _invoiceRowRepository.CommitAsync();

		await _invoiceService.UpdateInvoiceTotalSumAsync(invoiceRow.InvoiceId);
	}



	private async Task<InvoiceRow> CheckInvoiceRowIdAsync(Guid id, bool flag = false)
	{
		var invoiceRow = await _invoiceRowRepository.GetByIdAsync(id);

		if (invoiceRow is null || (flag && invoiceRow.IsDeleted))
			throw new NullReferenceException("The Invoice with This Id Doesn't Exsist !!");


		return invoiceRow;
	}

}
