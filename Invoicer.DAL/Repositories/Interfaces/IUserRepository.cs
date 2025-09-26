using Invoicer.Core.Entities;

namespace Invoicer.DAL.Repositories.Intrfaces;

public interface IUserRepository : IRepository<User>
{
	Task<bool?> EmailExsist(string? email);
}
