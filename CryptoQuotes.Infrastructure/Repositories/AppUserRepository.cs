using System.Linq;
using Domain.Entities.AppUser;
using Microsoft.EntityFrameworkCore;
using CryptoQuotes.Infrastructure.Identity;

namespace CryptoQuotes.Infrastructure.Repositories
{
    internal class AppUserRepository: RepositoryBase<User>, IAppUserRepository
    {
        public AppUserRepository(DataContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<User> GetQuery()
        {
            return base.GetQuery()
                .Include(u => u.IdentityUser);
        }
    }
}