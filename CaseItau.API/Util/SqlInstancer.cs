using System;
using System.Data.Common;
using System.Diagnostics;

namespace CaseItau.API.Util
{
    public class SqlInstancer
    {
        public static DbProviderFactory criaFactorySql()
        {            
            try 
            {
                const string providerName = "System.Data.SQLite";
                var factory = DbProviderFactories.GetFactory(providerName);
                return factory;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}