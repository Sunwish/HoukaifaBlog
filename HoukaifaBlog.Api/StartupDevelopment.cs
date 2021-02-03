using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoukaifaBlog.Core.Interfaces;
using HoukaifaBlog.Infrastructure.Database;
using HoukaifaBlog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HoukaifaBlog.Api
{
    public class StartupDevelopment
    {
        private readonly IConfiguration Configuration;

        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocalDB"));
            });

            services.AddScoped<IPostRepository, PostRepository>();

            // Enable Https Redirection
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
