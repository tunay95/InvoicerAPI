using Invoicer.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Invoicer.DAL.Repositories.Intrfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
	DbSet<TEntity> Table { get; }
	IQueryable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
	Task<TEntity> GetByIdAsync(Guid id);

	Task AddAsync(TEntity entity);
	void Update(TEntity entity);
	void Remove(TEntity entity);

	Task<int> CommitAsync();

}
