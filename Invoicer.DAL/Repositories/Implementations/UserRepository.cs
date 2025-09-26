using Invoicer.Core.Entities;
using Invoicer.DAL.Data;
using Invoicer.DAL.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.DAL.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{

	public UserRepository(InvoiceDbContext dbContext) : base(dbContext)
	{ }


	public async Task<bool?> EmailExsist(string? email)
		=> email == null
		? await Task.FromResult<bool?>(null)
		: await Table.AnyAsync(u => u.Email == email);
}
