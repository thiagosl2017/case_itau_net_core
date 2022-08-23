using CaseItau.API.Domain.Common.Interfaces;
using CaseItau.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CaseItau.API.Configurations
{
    public static class ConfigureDataBase
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<IitauDbContext, ItauDbContext>(options => options.UseSqlite("Data Source=dbCaseItau.s3db"));

            return services;
        }
    }
}
