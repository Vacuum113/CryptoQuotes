using System;

namespace Provider.Dtos
{
	public record CryptocurrencyDto
	{
		public int Id { get; init; }
		public string Name { get; init; }
		public int CoinMarketCapId { get; init; }
		public string Symbol { get; init; }
	}
}