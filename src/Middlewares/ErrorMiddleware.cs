using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PersonalPortfolio.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/NotFound";
                    await next(context);
                }
            }
            catch (Exception)
            {
                await next(context);
            }           
        }
    }
}
