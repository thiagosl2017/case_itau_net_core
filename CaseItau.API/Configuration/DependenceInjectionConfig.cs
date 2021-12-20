using CaseItau.API.Business.Services;
using CaseItau.API.Data.Repository;
using CaseItau.API.Util;
using Microsoft.Extensions.DependencyInjection;

namespace CaseItau.API.Configuration
{
    public static class DependenceInjectionConfig
    {
        public static IServiceCollection ResolveDependencias(this IServiceCollection service)
        {
            #region Services
            service.AddScoped<FundoService>();
            #endregion

            #region Repository
            service.AddScoped<SqlUtils>();
            service.AddScoped<FundoRepository>();
            #endregion

            return service;
        }
    }
}