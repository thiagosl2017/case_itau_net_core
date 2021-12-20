using CaseItau.API.Configuration;
using CaseItau.API.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Common;

namespace CaseItau.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DbProviderFactories.RegisterFactory("System.Data.SQLite", System.Data.SqlClient.SqlClientFactory.Instance);
            ConfigEstatica.Configuration = configuration;
            ConfigEstatica.DbFactory = SqlInstancer.criaFactorySql();
        }               
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.ResolveDependencias();

            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.WithOrigins("*"))
            );

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Case de Engenharia Itaú",
                    Version = "v2",
                    Description = "No projeto CaseItau.API foi disponibilizada uma API de Fundos com os seguintes métodos apresentados no swagger:",
                });
            });
            
        }          
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            
            app.UseRouting();
          
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllers(); 
            });

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "CaseItau Services"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}