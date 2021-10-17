using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Client.Forem.Base;
using PersonalPortfolio.Client.Forem.Models;

namespace PersonalPortfolio.Client.Forem.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IForemClient foremClient;

        public ArticleService(IForemClient foremClient)
        {
            this.foremClient = foremClient;
        }

        public Task<IEnumerable<Article>> GetArticlesByUserAsync(CancellationToken cancellationToken)
        {
            //string username, int page = 1, int perPage = 30
            var httpConfig = new HttpConfig
            {

            };

            //return foremClient.SendAsync();
            throw new NotImplementedException("This scenario has not been implemented");
        }
    }
}
