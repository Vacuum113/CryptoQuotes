using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Provider.Configuration;
using Provider.Dtos;

namespace Provider
{
	public interface ICryptoQuotesClient
	{
		Task<string> SendPostJson(string endpoint, object request);

		Task<string> SendGet(string endpoint);
	}

	public class CryptoQuotesClient : ICryptoQuotesClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly CoinMarketCapProviderConfiguration _providerOptions;

		public CryptoQuotesClient(IHttpClientFactory httpClientFactory, IOptions<CoinMarketCapProviderConfiguration> options)
		{
			_httpClientFactory = httpClientFactory;
			_providerOptions = options.Value;
		}

		public async Task<string> SendPostJson(string endpoint, object request)
		{
			return await PostJsonRequestInternal(endpoint, request);
		}

		public async Task<string> SendGet(string endpoint)
		{
			return await GetRequestInternal(endpoint);
		}
		
		private async Task<string> PostJsonRequestInternal(string endpoint, object request)
		{
			var content = new StringContent(JsonConvert.SerializeObject(request,
					new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
				Encoding.UTF8, "application/json");

			return await PostRequestInternal(endpoint, content);
		}

		private async Task<string> PostRequestInternal(string endpoint, HttpContent content)
		{
			using (var client = _httpClientFactory.CreateClient())
			{
				var message = ConfigureClient(client, HttpMethod.Post, endpoint);
				message.Content = content;
				
				var response = await client.SendAsync(message);
				var jsonString = await response.Content.ReadAsStringAsync();

				return jsonString;
			}
		}

		private async Task<string> GetRequestInternal(string endpoint)
		{
			using (var client = _httpClientFactory.CreateClient())
			{
				var message = ConfigureClient(client, HttpMethod.Get, endpoint);

				var response = await client.SendAsync(message);
				var jsonString = await response.Content.ReadAsStringAsync();

				return jsonString;
			}
		}

		private HttpRequestMessage ConfigureClient(HttpClient client, HttpMethod method, string endpoint)
		{
			client.BaseAddress = new Uri(_providerOptions.ApiUrl);
			var message = new HttpRequestMessage(method, endpoint);
			client.DefaultRequestHeaders.TryAddWithoutValidation("X-CMC_PRO_API_KEY", _providerOptions.Token);
			client.DefaultRequestHeaders.Add("Accepts", "application/json");

			return message;
		}
	}
}