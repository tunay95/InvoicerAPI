using AutoMapper;
using Invoicer.Business.DTOs.Base;
using Invoicer.Business.DTOs.CustomerDTOs;
using Invoicer.Business.Services.Interfaces;
using Invoicer.Core.Entities;
using Invoicer.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.Business.Services.Implementations;

public class CustomerService : ICustomerService
{
	private readonly ICustomerRepository _customerRepository;
	private readonly IMapper _mapper;

	public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
	{
		_customerRepository = customerRepository;
		_mapper = mapper;
	}


	public async Task<PaginationResponseDTO<CustomerResponseDTO?>> GetAllAsync(CustomerRequestDTO dto)
	{
		var customers = _customerRepository.GetAllAsync(c => !c.IsDeleted, c =>
												c.Include(c => c.Invoices.Where(ir => !ir.IsDeleted))
												.ThenInclude(i => i.InvoiceRows.Where(ir => !ir.IsDeleted)));

		if (dto.Search is not null)
			customers = customers.Where(u => u.Name.Contains(dto.Search) ||
									 u.Email.Contains(dto.Search) ||
									 u.PhoneNumber!.Contains(dto.Search));

		int totalCount = await customers.CountAsync();

		if (dto.PageNumber is not null && dto.PageSize is not null)
			customers = customers
				.Skip((dto.PageNumber.Value - 1) * dto.PageSize.Value)
				.Take(dto.PageSize.Value);


		return new PaginationResponseDTO<CustomerResponseDTO>
		{
			Entities = _mapper.Map<IEnumerable<CustomerResponseDTO>>(customers),
			TotalCount = totalCount,
			PageNumber = dto.PageNumber ?? 1,
			PageSize = dto.PageSize ?? (totalCount == 0 ? 1 : totalCount)
		}!;
	}


	public async Task<CustomerResponseDTO?> GetAsync(string? search)
	{
		var customers = _customerRepository.GetAllAsync(u => u.IsDeleted == false);

		if (search is not null)
			customers = customers.Where(u => u.Name.Contains(search) ||
									 u.Email.Contains(search) ||
									 u.PhoneNumber!.Contains(search));


		return _mapper.Map<CustomerResponseDTO>(await customers.FirstOrDefaultAsync());
	}


	public async Task<CustomerResponseDTO?> GetByIdAsync(Guid id)
	{
		var customer = await CheckCustomerIdAsync(id, true);

		return _mapper.Map<CustomerResponseDTO?>(customer);
	}


	public async Task<CustomerResponseDTO?> CreateAsync(CreateCustomerDTO? dto)
	{
		if (dto is null)
			throw new ArgumentNullException(nameof(dto));

		var result = await _customerRepository.EmailExsist(dto.Email);


		switch (result)
		{
			case null:
				throw new ArgumentNullException(nameof(dto.Email), "There's no Email, You should enter Some Email !!");

			case true:
				throw new InvalidOperationException("This Email is already in Use !!");

			case false:
				var customer = _mapper.Map<Customer>(dto);

				customer.CreatedAt = DateTimeOffset.UtcNow;


				await _customerRepository.AddAsync(customer);

				await _customerRepository.CommitAsync();

				return _mapper.Map<CustomerResponseDTO?>(customer);


			default:
				throw new Exception("Some Problems ..");
		}

	}


	public async Task<CustomerResponseDTO?> UpdateAsync(Guid id, UpdateCustomerDTO? dto)
	{
		if (dto is null)
			throw new ArgumentNullException(nameof(dto), "The Dto should not be Null !!");

		var previousCustomer = await CheckCustomerIdAsync(id);
		_mapper.Map(dto, previousCustomer);

		previousCustomer.UpdatedAt = DateTimeOffset.UtcNow;


		_customerRepository.Update(previousCustomer);

		await _customerRepository.CommitAsync();

		return _mapper.Map<CustomerResponseDTO>(previousCustomer);
	}


	public async Task SoftDeleteAsync(Guid id)
	{
		var customer = await CheckCustomerIdAsync(id);


		customer.IsDeleted = !customer.IsDeleted
			? true
			: throw new InvalidOperationException("The Customer had already been Soft-Deleted !!");


		customer.DeletedAt = DateTimeOffset.UtcNow;

		await _customerRepository.CommitAsync();
	}


	public async Task RecoverAsync(Guid id)
	{
		var customer = await CheckCustomerIdAsync(id);


		customer.IsDeleted = customer.IsDeleted
			? false
			: throw new InvalidOperationException("The Customer had already been Recovered !!");


		customer.DeletedAt = null;

		await _customerRepository.CommitAsync();
	}


	public async Task RemoveAsync(Guid id)
	{
		var customer = await CheckCustomerIdAsync(id);

		_customerRepository.Remove(customer);

		await _customerRepository.CommitAsync();
	}



	private async Task<Customer> CheckCustomerIdAsync(Guid id, bool flag = false)
	{
		var customer = await _customerRepository.GetByIdAsync(id);

		if (customer is null || (flag && customer.IsDeleted))
			throw new NullReferenceException("The Customer with This Id Doesn't Exsist !!");


		return customer;
	}

}
