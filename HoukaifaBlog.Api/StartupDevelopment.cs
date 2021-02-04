using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using HoukaifaBlog.Api.Extensions;
using HoukaifaBlog.Core.Interfaces;
using HoukaifaBlog.Infrastructure.Database;
using HoukaifaBlog.Infrastructure.Repositories;
using HoukaifaBlog.Infrastructure.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

                // Return 406 when request output format not acceptable, and enable xml output
                options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocalDB"));
            });

            // Enable Https Redirection
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Map Entities to Resources
            services.AddAutoMapper();

            // Resources validator
            services.AddTransient<IValidator<PostResource>, PostResourceValidator>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            //app.UseDeveloperExceptionPage();
            app.UseMyExceptionHandler(loggerFactory);

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
