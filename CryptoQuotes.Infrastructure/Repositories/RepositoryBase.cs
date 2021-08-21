using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;

namespace CryptoQuotes.Infrastructure.Repositories
{
	internal abstract class RepositoryBase<TDomainEntity> : IGetOneByIdRepository<TDomainEntity>, IGetManyByIdsRepository<TDomainEntity>,
		IUpdateRepository<TDomainEntity>, IAddRepository<TDomainEntity>, IFilterRepository<TDomainEntity>
		where TDomainEntity : class, IEntity<int>
	{
		protected readonly DataContext DbContext;

		protected RepositoryBase(DataContext dbContext)
		{
			DbContext = dbContext;
		}

		public virtual async Task<TDomainEntity> Get(int id) =>
			await GetQuery().FirstOrDefaultAsync(e => e.Id == id);

		public virtual async Task<IList<TDomainEntity>> Get(IList<int> ids)
		{
			return await GetQuery()
				.Where(e => ids.Contains(e.Id))
				.ToListAsync();
		}

		public Task Update(TDomainEntity entity)
		{
			DbContext.MarkAsChanged(entity);

			return Task.CompletedTask;
		}

		public Task Add(TDomainEntity entity) => Task.FromResult(GetSet().Add((TDomainEntity) entity));

		protected virtual DbSet<TDomainEntity> GetSet() => DbContext.Set<TDomainEntity>();

		protected virtual IQueryable<TDomainEntity> GetQuery() => GetSet();

		public async Task<IList<TDomainEntity>> GetByFilter(Expression<Func<TDomainEntity, bool>> conditions) =>
			await GetQuery().Where(conditions).ToListAsync();

		protected Expression<Func<TDomainEntity, TDomainEntity>> TypeConverter => (e) => (TDomainEntity)e;
	}
}