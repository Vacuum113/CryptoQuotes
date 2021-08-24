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

        public override IQueryable<Cryptocurrency> Sort(IQueryable<Cryptocurrency> query, EntityRequest<CryptocurrencyRequest> request)
        {
            switch (request.Order)
            {
                case "Price":
                    query = request.OrderBy != null && request.OrderBy == SortOrder.Asc 
                        ? query.OrderBy(q => q.CryptoQuote.First(cq => cq.IsActual).Price)
                        : query.OrderByDescending(q => q.CryptoQuote.First(cq => cq.IsActual).Price);
                    break;
                case "PercentChangeOneHour":
                    query = request.OrderBy != null && request.OrderBy == SortOrder.Asc 
                        ? query.OrderBy(q => q.CryptoQuote.First(cq => cq.IsActual).PercentChangeOneHour)
                        : query.OrderByDescending(q => q.CryptoQuote.First(cq => cq.IsActual).PercentChangeOneHour);
                    break;
                case "PercentChangeTwentyFourHours":
                    query = request.OrderBy != null && request.OrderBy == SortOrder.Asc 
                        ? query.OrderBy(q => q.CryptoQuote.First(cq => cq.IsActual).PercentChangeTwentyFourHours)
                        : query.OrderByDescending(q => q.CryptoQuote.First(cq => cq.IsActual).PercentChangeTwentyFourHours);
                    break;
                case "MarketCap":
                    query = request.OrderBy != null && request.OrderBy == SortOrder.Asc 
                        ? query.OrderBy(q => q.CryptoQuote.First(cq => cq.IsActual).MarketCap)
                        : query.OrderByDescending(q => q.CryptoQuote.First(cq => cq.IsActual).MarketCap);
                    break;
                case "LastUpdated":
                    query = request.OrderBy != null && request.OrderBy == SortOrder.Asc 
                        ? query.OrderBy(q => q.CryptoQuote.First(cq => cq.IsActual).LastUpdated)
                        : query.OrderByDescending(q => q.CryptoQuote.First(cq => cq.IsActual).LastUpdated);
                    break;
                default:
                    query = base.Sort(query, request);
                    break;
            }

            return query;
        }

        public override IQueryable<Cryptocurrency> GetQuery()
        {
            return base.GetQuery()
                .Include(c => c.CryptoQuote);
        }
    }
}