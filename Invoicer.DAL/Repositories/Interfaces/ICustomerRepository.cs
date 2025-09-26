using Invoicer.Core.Entities;
using Invoicer.DAL.Repositories.Intrfaces;

namespace Invoicer.DAL.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
	Task<bool?> EmailExsist(string? email);
}
