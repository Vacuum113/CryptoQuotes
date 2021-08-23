using System.Linq;
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
                .ForMember(m => m.CryptoQuoteId, opt => opt.MapFrom(vm => vm.CryptoQuote.First().Id))
                .ForMember(m => m.Price, opt => opt.MapFrom(vm => vm.CryptoQuote.First().Price))
                .ForMember(m => m.PercentChangeOneHour, opt => opt.MapFrom(vm => vm.CryptoQuote.First().PercentChangeOneHour))
                .ForMember(m => m.PercentChangeTwentyFourHours, opt => opt.MapFrom(vm => vm.CryptoQuote.First().PercentChangeTwentyFourHours))
                .ForMember(m => m.MarketCap, opt => opt.MapFrom(vm => vm.CryptoQuote.First().MarketCap))
                .ForMember(m => m.IsActual, opt => opt.MapFrom(vm => vm.CryptoQuote.First().IsActual))
                .ForMember(m => m.LastUpdated, opt => opt.MapFrom(vm => vm.CryptoQuote.First().LastUpdated))
                .ReverseMap();
        }
    }
}