using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.Client.Forem
{
    public class ForemClient : IForemClient
    {
        private readonly HttpClient httpClient;

        public ForemClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancelationToken)
        {
            throw new NotImplementedException();
        }
    }
}
