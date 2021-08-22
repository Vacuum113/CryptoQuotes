using Domain.Abstractions.Repository;
using Domain.Entities.AppUser;

namespace Domain.Entities.CryptoQuote
{
	public interface ICryptoQuoteRepository : IUpdateRepository<CryptoQuote>, IGetOneByIdRepository<CryptoQuote>, IFilterRepository<CryptoQuote>, IAddRepository<CryptoQuote>
	{
	}
}