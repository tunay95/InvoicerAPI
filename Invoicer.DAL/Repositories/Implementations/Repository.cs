using Invoicer.Core.Entities.Common;
using Invoicer.DAL.Data;
using Invoicer.DAL.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Invoicer.DAL.Repositories.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{
	private readonly InvoiceDbContext _dbContext;

	public Repository(InvoiceDbContext dbContext)
	{
		_dbContext = dbContext;
	}



	public DbSet<TEntity> Table => _dbContext.Set<TEntity>();


	public IQueryable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
	{
		var query = Table.AsQueryable();

		if (include is not null)
			query = include(query);


		return filter is not null
			? query.Where(filter)
			: query;
	}

	public async Task<TEntity> GetByIdAsync(Guid id) => await Table.FindAsync(id);



	public async Task AddAsync(TEntity entity) => await Table.AddAsync(entity);

	public void Update(TEntity entity) => Table.Update(entity);

	public void Remove(TEntity entity) => Table.Remove(entity);


	public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();

}
