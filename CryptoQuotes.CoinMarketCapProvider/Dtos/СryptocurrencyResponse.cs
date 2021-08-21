using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Provider.Dtos
{
	public record СryptocurrencyResponse
	{
		public Status Status { get; init; }
		public List<CryptocurrencyData> Data { get; init; }
	}

	public record CryptocurrencyData
	{
		public int Id { get; init; }
		public string Name { get; init; }
		public string Symbol { get; init; }
		public Quote Quote { get; init; }
	}

	public record Quote
	{
		public USD Usd { get; init; }
	}

	public record USD
	{
		public decimal Price { get; init; }
		public float PercentChange_1h { get; init; }
		public float PercentChange_24h { get; init; }
		public decimal MarketCap { get; init; }
	}

	public record Status
	{
		public int ErrorCode { get; init; }
		public string ErrorMessage { get; init; }
	}
}