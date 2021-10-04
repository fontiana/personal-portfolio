using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.Client.Forem
{
    public interface IForemClient
    {
        Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancelationToken);
    }
}
