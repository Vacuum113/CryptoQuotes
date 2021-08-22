using Application.UseCases.Queries.CryptocurrencyQuery;
using AutoMapper;
using Domain.Entities.Cryptocurrency;

namespace Application.MapperProfiles
{
    internal class CryptocurrencyProfile : Profile
    {
        public CryptocurrencyProfile()
        {
            CreateMap<Cryptocurrency, CryptocurrencyResponse>()
                .ReverseMap();
        }
    }
}