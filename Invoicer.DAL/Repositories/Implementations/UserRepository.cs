using Invoicer.Core.Entities;
using Invoicer.DAL.Data;
using Invoicer.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.DAL.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{

	public UserRepository(InvoiceDbContext dbContext) : base(dbContext)
	{ }


	public async Task<bool?> EmailExsist(string? email)
	{
		if (email is null)
			return null;

		return await Table.AnyAsync(u => u.Email == email);
	}
}
