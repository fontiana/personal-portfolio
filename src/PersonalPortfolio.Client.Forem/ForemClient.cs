using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PersonalPortfolio.Client.Forem.Base;

namespace PersonalPortfolio.Client.Forem
{
    public class ForemClient : IForemClient
    {
        private readonly HttpClient httpClient;
        private readonly ForemConfig config;

        public ForemClient(HttpClient httpClient, IOptions<ForemConfig> options)
        {
            this.httpClient = httpClient;
            this.config = options.Value;
        }

        public async Task<TResponse> SendAsync<TResponse>(HttpConfig httpConfig, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage
            {
                Method = httpConfig.HttpMethod,
                RequestUri = new Uri($"{config.Uri}{httpConfig.Path}")
            };

            message.Headers.Add("api-key", config.ApiKey);
            message.Headers.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.1 Safari/605.1.15");

            using var response = await httpClient.SendAsync(message);

            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResponse>(responseJson);
            }
            else
            {
                throw new ForemException(response.ReasonPhrase, responseJson, response.StatusCode);
            }
        }
    }
}
