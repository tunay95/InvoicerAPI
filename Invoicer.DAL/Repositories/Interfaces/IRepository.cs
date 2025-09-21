using Invoicer.Core.Entities.Common;
using System;
using System.Linq.Expressions;

namespace Invoicer.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
	IQueryable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, params string[] includes);
	Task<TEntity> GetByIdAsync(Guid id);

	Task AddAsync(TEntity entity);
	void Update(TEntity entity);
	void Remove(TEntity entity);

	Task<int> CommitAsync();

}
