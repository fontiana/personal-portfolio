using System;
using System.Net.Http;

namespace PersonalPortfolio.Client.Forem.Base
{
    public class HttpConfig
    {
        public HttpMethod HttpMethod { get; set; }
        public string Path { get; set; }
        public object Body { get; set; }
    }
}
