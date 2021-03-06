using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoQuotes.Infrastructure.Identity;
using Domain.Entities.AppUser;
using Domain.Entities.IdentityAppUser;
using Microsoft.EntityFrameworkCore;

namespace CryptoQuotes.Infrastructure.Repositories
{
	internal class IdentityAppUserRepository : RepositoryBase<IdentityAppUser>, IIdentityAppUserRepository
	{
		public IdentityAppUserRepository(DataContext dbContext) : base(dbContext)
		{
		}

		public async Task<IIdentityUser> FindByIdentityId(int id)
		{
			return await GetQuery().FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<IIdentityUser> FindByEmailAsync(string email)
		{
			return await GetQuery().FirstOrDefaultAsync(u => u.Email == email);
		}

		public Task Update(IIdentityUser entity)
		{
			GetSet().Update(entity as IdentityAppUser);

			return Task.CompletedTask;
		}

		public Task UpdateRange(IEnumerable<IIdentityUser> entities)
		{
			GetSet().UpdateRange(entities.Cast<IdentityAppUser>());

			return Task.CompletedTask;
		}

		public async Task<IIdentityUser> Get(int id) =>
			await GetQuery().FirstOrDefaultAsync(e => e.Id == id);

		public async Task<IList<IIdentityUser>> GetByFilter(Expression<Func<IIdentityUser, bool>> conditions) =>
			await GetQuery().Where(conditions).ToListAsync();
	}
}