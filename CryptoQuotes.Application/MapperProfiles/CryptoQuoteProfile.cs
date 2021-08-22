using Application.UseCases.Queries.CryptoQuoteQuery;
using AutoMapper;
using Domain.Entities.CryptoQuote;

namespace Application.MapperProfiles
{
    internal class CryptoQuoteProfile : Profile
    {
        public CryptoQuoteProfile()
        {
            CreateMap<CryptoQuote, CryptoQuoteResponse>()
                .ReverseMap();
        }
    }
}