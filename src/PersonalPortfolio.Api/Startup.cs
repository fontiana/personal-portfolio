using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PersonalPortfolio.Middlewares;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using FluentValidation.AspNetCore;
using PersonalPortfolio.Areas.Admin.Validator;
using PersonalPortfolio.Context;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Repository.Project;
using PersonalPortfolio.Repository.Post;
using Serilog;
using Serilog.Events;
using PersonalPortfolio.Helper;
using PersonalPortfolio.Client.Forem;
using PersonalPortfolio.Client.Forem.Services;

namespace PersonalPortfolio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            services.AddMicrosoftWebAppAuthentication(Configuration);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddDbContext<PortfolioContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PortfolioContext")));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IImageHelper, ImageHelper>();
            AddForem(services);

            var mvcBuilder = services
                .AddControllersWithViews(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<ProjectValidator>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddMicrosoftIdentityUI()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif
        }

        private void AddForem(IServiceCollection services)
        {
            var foremConfig = Configuration.GetSection("DevTo");
            services.Configure<ForemConfig>(foremConfig);

            services.AddHttpClient<IForemClient, ForemClient>();
            services.AddScoped<IArticleService, ArticleService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PortfolioContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/index");
                app.UseHsts();
            }
            app.UseMiddleware<ErrorMiddleware>();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseLocalization();

            context.Database.EnsureCreated();

            app.UseCors();

            app.UseRouter(router =>
            {
                router.MapGet(".well-known/acme-challenge/{id}", async (request, response, routeData) =>
                {
                    var id = routeData.Values["id"] as string;
                    var file = Path.Combine(env.WebRootPath, ".well-known", "acme-challenge", id);
                    await response.SendFileAsync(file);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "category",
                    pattern: "home/blog/category/{id?}");
            });
        }
    }
}
