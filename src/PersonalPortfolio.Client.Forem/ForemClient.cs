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

        public async Task<TResponse> SendAsync<TResponse>(HttpConfig httpConfig, CancellationToken cancelationToken)
        {
            var message = new HttpRequestMessage
            {
                Method = httpConfig.HttpMethod
            };

            message.Headers.Add("api-key", config.ApiKey);

            using var response = await httpClient.SendAsync(message);

            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResponse>(responseJson);
            }

            throw new NotImplementedException("This scenario has not been implemented");
        }
    }
}
