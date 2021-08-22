using System.Linq;
using Domain.Entities.CryptoQuote;
using Microsoft.EntityFrameworkCore;

namespace CryptoQuotes.Infrastructure.Repositories
{
    internal class CryptoQuoteRepository : RepositoryBase<CryptoQuote>, ICryptoQuoteRepository
    {
        public CryptoQuoteRepository(DataContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<CryptoQuote> GetQuery()
        {
            return base.GetQuery()
                .Include(u => u.Cryptocurrency);
        }
    }
}