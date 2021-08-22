using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Provider.Configuration;
using Provider.Dtos;

namespace Provider
{
	public interface ICryptoQuotesProvider
	{
		Task<СryptocurrencyResponse> GetLatest();

		Task<СryptocurrencyResponse> GetByTicketId(string ticketId);
	}

	public class CryptoQuotesProvider : ICryptoQuotesProvider
	{
		private readonly ICryptoQuotesClient _client;
		
		private static JsonSerializerSettings _settings = new ()
		{
			ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
			Formatting = Formatting.Indented,
			
		};
		
		public CryptoQuotesProvider(ICryptoQuotesClient client)
		{
			_client = client;
		}

		public async Task<СryptocurrencyResponse> GetLatest()
		{
			var result = await _client.SendGet("cryptocurrency/listings/latest?limit=5000");
			return JsonConvert.DeserializeObject<СryptocurrencyResponse>(result, _settings);
		}

		public async Task<СryptocurrencyResponse> GetByTicketId(string ticketId)
		{
			var result = await _client.SendGet($"show/{ticketId}");
			return JsonConvert.DeserializeObject<СryptocurrencyResponse>(result, _settings);
		}
	}
}