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
		public DateTime LastUpdated { get; init; }
	}

	public record Status
	{
		public ResponseStatusCode ErrorCode { get; init; }
		public string ErrorMessage { get; init; }
	}

	public enum ResponseStatusCode
	{
		Success = 0,
		ApiKeyInvalid = 1001,
		ApiKeyMissing = 1002,
		ApiKeyPlanRequiresPayment = 1003,
		ApiKeyPlanPaymentExpired = 1004,
		ApiKeyRequired = 1005,
		ApiKeyPlanNotAuthorized = 1006,
		ApiKeyDisabled = 1007,
		ApiKeyPlanMinuteRateLimitReached = 1008,
		ApiKeyPlanDailyRateLimitReached = 1009,
		ApiKeyPlanMonthlyRateLimitReached = 1010,
		IpRateLimitReached = 1011
	}
}