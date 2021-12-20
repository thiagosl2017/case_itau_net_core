using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;

namespace CaseItau.API.Util
{
    public sealed class SqlUtils
    {      
        public SqlUtils() { }
        public DbConnection criaConexaoSql()
        {
            try
            {              
                var connection = new SQLiteConnection("Data Source=dbCaseItau.s3db");
                
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return connection;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}