using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace CaseItau.API.Util
{
    public class ConfigEstatica
    {
        public static IConfiguration Configuration;
        public static DbProviderFactory DbFactory;
    }
}