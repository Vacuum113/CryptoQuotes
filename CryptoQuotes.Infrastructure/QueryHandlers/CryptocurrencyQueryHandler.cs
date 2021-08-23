using System.Linq;
using System.Threading.Tasks;
using Application.UseCases.Queries.Abstractions;
using Application.UseCases.Queries.CryptocurrencyQuery;
using AutoMapper;
using CryptoQuotes.Infrastructure.QueryHandlers.Abstractions;
using Domain.Entities.Cryptocurrency;
using Microsoft.EntityFrameworkCore;

namespace CryptoQuotes.Infrastructure.QueryHandlers
{
    public class CryptocurrencyQueryHandler : EntityQueryHandlerBase<CryptocurrencyRequest, CryptocurrencyResponse, Cryptocurrency>, ICryptocurrencyQueryHandler
    {
        public CryptocurrencyQueryHandler(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override CryptocurrencyResponse MapEntity(Cryptocurrency entity)
        {
            // t: get only actual quote from db
            entity.SetCryptoQuote(entity.CryptoQuote.Where(cq => cq.IsActual));
            var result = Mapper.Map<Cryptocurrency, CryptocurrencyResponse>(entity);
            return result;
        }

        public override IQueryable<Cryptocurrency> GetQuery()
        {
            return base.GetQuery()
                .Include(c => c.CryptoQuote);
        }
    }
}