using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<IEnumerable<Article>> GetArticlesByUserAsync(string username, int page = 1, int perPage = 30)
        {
            //return foremClient.SendAsync();
            throw new NotImplementedException();
        }
    }
}
