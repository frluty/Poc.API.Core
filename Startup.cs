using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NJsonSchema;
using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Poc.API.Core.Models;
using Poc.API.Core.Data;
using Poc.API.Core.Dao;
using Poc.API.Core.Dao.Interfaces;
using Poc.API.Core.Dto;
using Poc.API.Core.Dao.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Poc.API.Core
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
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddMvc();
            services.AddScoped<IProduitDao, ProduitDao>();
            services.AddTransient<IRepository<ProduitDto>, Repository<Produit, ProduitDto>>();

            #region AutoMapper
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProduitDto, Produit>();
                cfg.CreateMap<Produit, ProduitDto>();

            });
            #endregion

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Kerialis API";
                    document.Info.Description = "Test technique Kerialis .NET CORE 3";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Luty FRANCOIS",
                        Email = string.Empty,
                        Url = "https://www.linkedin.com/in/luty-francois-491a5710b/"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "FrLuty",
                        Url = ""
                    };
                };
            });
                        
            services.AddSingleton(new FluentNHibernateHelper(Configuration.GetConnectionString("DB"), true));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger(@"C:\API\test.log");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
