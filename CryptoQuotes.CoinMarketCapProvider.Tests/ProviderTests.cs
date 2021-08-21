using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Provider;
using Provider.Configuration;

namespace CryptoQuotes.CoinMarketCapProvider.Tests
{
    public class Tests
    {
        private string _token;
        
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Tests>()
                .Build();

            _token = configuration.GetValue<string>("CoinMarketCapProvider:Token");
        }

        [Test]
        public async Task Should_Return_Latest_Cryptocurrency()
        {
            var options = new Mock<IOptions<CoinMarketCapProviderConfiguration>>();
            options.Setup(o => o.Value).Returns(new CoinMarketCapProviderConfiguration()
                {ApiUrl = "https://pro-api.coinmarketcap.com/v1/", Token = _token});
            var provider = new CryptoQuotesProvider(new CryptoQuotesClient(new DefaultHttpClientFactory(), options.Object));
            var result = await provider.GetLatest();
            
            Assert.Zero(result.Status.ErrorCode);
            Assert.Null(result.Status.ErrorMessage);
            Assert.NotZero(result.Data.Count);
            Assert.NotZero(result.Data.First().Quote.Usd.PercentChange_1h);
            Assert.NotZero(result.Data.First().Quote.Usd.PercentChange_24h);
        }

        public sealed class DefaultHttpClientFactory : IHttpClientFactory
        {
            public HttpClient CreateClient(string name)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                return new HttpClient(clientHandler);
            }
        }
    }
}