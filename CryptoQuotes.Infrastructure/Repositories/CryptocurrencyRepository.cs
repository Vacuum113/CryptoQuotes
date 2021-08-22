using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Cryptocurrency;
using Microsoft.EntityFrameworkCore;

namespace CryptoQuotes.Infrastructure.Repositories
{
    internal class CryptocurrencyRepository : RepositoryBase<Cryptocurrency>, ICryptocurrencyRepository
    {
        public CryptocurrencyRepository(DataContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Cryptocurrency> GetQuery()
        {
            return base.GetQuery()
                .Include(u => u.CryptoQuote);
        }

        public async Task<IEnumerable<Cryptocurrency>> All()
        {
            return await GetQuery().ToListAsync();
        }
    }
}