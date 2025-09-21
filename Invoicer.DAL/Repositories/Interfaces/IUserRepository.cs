using Invoicer.Core.Entities;

namespace Invoicer.DAL.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
	Task<bool?> EmailExsist(string? email);
}
