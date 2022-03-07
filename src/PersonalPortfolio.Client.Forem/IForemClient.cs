using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Client.Forem.Base;

namespace PersonalPortfolio.Client.Forem
{
    public interface IForemClient
    {
        Task<TResponse> SendAsync<TResponse>(HttpConfig httpConfig, CancellationToken cancellationToken);
    }
}
