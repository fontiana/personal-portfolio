using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PersonalPortfolio.Client.Forem.Base;
using PersonalPortfolio.Client.Forem.Models;

namespace PersonalPortfolio.Client.Forem.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IForemClient foremClient;
        private readonly ForemConfig config;

        public ArticleService(IForemClient foremClient, IOptions<ForemConfig> options)
        {
            this.foremClient = foremClient;
            this.config = options.Value;
        }

        public async Task<IEnumerable<Article>> GetArticlesByUserAsync(int page,
                                                                       int perPage,
                                                                       CancellationToken cancellationToken)
        {
            var httpConfig = new HttpConfig
            {
                 HttpMethod = HttpMethod.Get,
                 Path = $"/api/articles?page={page}&per_page={perPage}&username={config.Username}"
            };

            return await foremClient.SendAsync<IEnumerable<Article>>(httpConfig, cancellationToken);
        }
    }
}
