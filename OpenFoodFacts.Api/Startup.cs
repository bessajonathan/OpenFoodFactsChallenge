using Hangfire;
using Hangfire.Storage.SQLite;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenFoodFacts.Api.Filters;
using OpenFoodFacts.Api.PreRequest;
using OpenFoodFacts.Application.Job.Command;
using OpenFoodFacts.Application.Service;
using OpenFoodFacts.Application.Service.Interfaces;
using OpenFoodFacts.Common.Configurations;
using OpenFoodFacts.Domain.Repository.Interfaces;
using OpenFoodFacts.Domain.Service;
using OpenFoodFacts.Domain.Service.Interfaces;
using OpenFoodFacts.Infra.Integrations.Interfaces;
using OpenFoodFacts.Infra.Integrations.OpenFood;
using OpenFoodFacts.Persistence.Context;
using OpenFoodFacts.Persistence.Interfaces;
using OpenFoodFacts.Persistence.Repository;
using System;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using NSwag;
using OpenFoodFacts.API.Filters;
using OpenFoodFacts.API.Services;
using OpenFoodFacts.Application.Configurations;
using OpenFoodFacts.Application.Product.Queries.Get;


namespace OpenFoodFacts.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.Configure<OpenFoodSettings>(Configuration.GetSection("OpenFoodSettings"));
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddOpenApiDocument(x =>
            {
                x.Title = "Open Food Facts";
                x.Description = "Informação nutricional de diversos produtos alimentícios.";
            });

            services.AddCors();

            //Adicionando MediatR
            services.AddMediatR(typeof(DownloadProductsCommand).GetTypeInfo().Assembly);

            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(AuthorizationFilter));
                    options.Filters.Add(typeof(ExceptionFilter));
                })
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
            .AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<GetProductQueryValidator>());

            services.AddAutoMapper(typeof(AutoMapperProfiles));

            services.AddHangfire(config =>
                config.UseSQLiteStorage(Configuration.GetConnectionString("HangfireDatabase")));

            services.AddDbContext<IOpenFoodFactsContext, OpenFoodFactsContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("OpenFoodDatabase")));

            services.AddTransient<IJobApplicationService, JobApplicationService>();
            services.AddTransient<IApiApplicationService, ApiApplicationService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IOpenFoodService, OpenFoodService>();
            services.AddTransient<IOpenFoodApplicationService, OpenFoodApplicationService>();
            services.AddTransient<IOpenFoodProvider, OpenFoodProvider>();
            services.AddTransient<IOpenFoodFactsContext, OpenFoodFactsContext>();

            services.AddTransient<IOpenFoodRepository, OpenFoodRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/logs.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(option =>
            {
                option.AllowAnyOrigin();
                option.AllowAnyHeader();
                option.AllowAnyMethod();
            });
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseReDoc(x =>
            {
                x.Path = "/redoc";
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                ServerName = string.Format("{0}.{1}", Environment.MachineName, Guid.NewGuid().ToString())
            });

            app.UseHangfireDashboard("/jobs");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            new BackgroundJobs(app.ApplicationServices.GetService<IMediator>(), app.ApplicationServices.GetService<IConfiguration>()).StartJobs();
        }
    }
}
