using CaseItau.API.Model;
using CaseItau.API.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace CaseItau.API.Data.Repository
{
    public class FundoRepository
    {
        #region Atributos
        private readonly SqlUtils _sqlUtil;
        #endregion

        #region Construtor
        public FundoRepository(SqlUtils sqlUtil)
        {
            _sqlUtil = sqlUtil;
        }
        #endregion

        #region Métodos
        public async Task<List<Fundo>> ObterTodosFundos()
        {
            var dados = new List<Fundo>();

            try
            {
                using (DbConnection conexao = _sqlUtil.criaConexaoSql())
                {
                    using (var cmd = conexao.CreateCommand())
                    {
                        cmd.CommandText = "SELECT F.*, T.NOME AS NOME_TIPO FROM FUNDO F INNER JOIN TIPO_FUNDO T ON T.CODIGO = F.CODIGO_TIPO";
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = conexao;

                        using DbDataReader reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            var f = new Fundo();
                            f.Codigo = reader[0].ToString();
                            f.Nome = reader[1].ToString();
                            f.Cnpj = reader[2].ToString();
                            f.CodigoTipo = reader.IsDBNull(3) ? 0 : int.Parse(reader[3].ToString());
                            f.Patrimonio = reader.IsDBNull(4) ? 0 : decimal.Parse(reader[4].ToString());
                            f.NomeTipo = reader[5].ToString();
                            dados.Add(f);
                        }
                    }
                }
                return dados;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<List<Fundo>> ObterFundoPorCodigo (string codigo)
        {
            var dados = new List<Fundo>();

            try
            {
                using (DbConnection conexao = _sqlUtil.criaConexaoSql())
                {
                    using (var cmd = conexao.CreateCommand())
                    {
                        var param = new SQLiteParameter
                        {
                            ParameterName = "@CODIGO",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = codigo
                        };

                        cmd.Parameters.Add(param);

                        cmd.CommandText = "SELECT F.*, T.NOME AS NOME_TIPO FROM FUNDO F INNER JOIN TIPO_FUNDO T ON T.CODIGO = F.CODIGO_TIPO WHERE F.CODIGO = @CODIGO";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conexao;

                        using DbDataReader reader = await cmd.ExecuteReaderAsync();
                        if (reader.Read())
                        {
                            var f = new Fundo();
                            f.Codigo = reader[0].ToString();
                            f.Nome = reader[1].ToString();
                            f.Cnpj = reader[2].ToString();
                            f.CodigoTipo = int.Parse(reader[3].ToString());
                            f.Patrimonio = decimal.Parse(reader[4].ToString());
                            f.NomeTipo = reader[5].ToString();
                            dados.Add(f);
                            return dados;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CriarFundo([FromBody] Fundo value)
        {
            try
            {
                using (DbConnection conexao = _sqlUtil.criaConexaoSql())
                {
                    using (var cmd = conexao.CreateCommand())
                    {
                        var param = new SQLiteParameter
                        {
                            ParameterName = "@CODIGO",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = value.Codigo
                        };

                        cmd.Parameters.Add(param);

                        param = new SQLiteParameter
                        {
                            ParameterName = "@NOME",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = value.Nome
                        };

                        cmd.Parameters.Add(param);

                        param = new SQLiteParameter
                        {
                            ParameterName = "@CNPJ",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = value.Cnpj
                        };

                        cmd.Parameters.Add(param);

                        param = new SQLiteParameter
                        {
                            ParameterName = "@CODIGO_TIPO",
                            DbType = DbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = value.CodigoTipo
                        };

                        cmd.Parameters.Add(param);

                        param = new SQLiteParameter
                        {
                            ParameterName = "@PATRIMONIO",
                            DbType = DbType.Decimal,
                            Direction = ParameterDirection.Input,
                            Value = value.Patrimonio
                        };

                        cmd.Parameters.Add(param);

                        cmd.CommandText = "INSERT INTO FUNDO VALUES(@CODIGO, @NOME, @CNPJ, @CODIGO_TIPO, @PATRIMONIO)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conexao;

                        var success = await cmd.ExecuteNonQueryAsync();
                        if(success > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> MovimentarFundo(string codigo, decimal? patrimonio)
        {
            try
            {
                using (DbConnection conexao = _sqlUtil.criaConexaoSql())
                {
                    using (var cmd = conexao.CreateCommand())
                    {
                        var param = new SQLiteParameter
                        {
                            ParameterName = "@PATRIMONIO",
                            DbType = DbType.Decimal,
                            Direction = ParameterDirection.Input,
                            Value = patrimonio
                        };

                        cmd.Parameters.Add(param);

                        param = new SQLiteParameter
                        {
                            ParameterName = "@CODIGO",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = codigo
                        };

                        cmd.Parameters.Add(param);                     

                        cmd.CommandText = "UPDATE FUNDO SET PATRIMONIO = IFNULL(PATRIMONIO,0) + @PATRIMONIO WHERE CODIGO = @CODIGO";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conexao;

                        var success = await cmd.ExecuteNonQueryAsync();
                        if (success > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> ExcluirFundo(string codigo)
        {
            try
            {
                using (DbConnection conexao = _sqlUtil.criaConexaoSql())
                {
                    using (var cmd = conexao.CreateCommand())
                    {
                        var param = new SQLiteParameter
                        {
                            ParameterName = "@CODIGO",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = codigo
                        };

                        cmd.Parameters.Add(param);

                        cmd.CommandText = "DELETE FROM FUNDO WHERE CODIGO = @CODIGO";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conexao;

                        var success = await cmd.ExecuteNonQueryAsync();
                        if (success > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AlterarFundo(string codigo, [FromBody] Fundo value)
        {
            try
            {
                using (DbConnection conexao = _sqlUtil.criaConexaoSql())
                {
                    using (var cmd = conexao.CreateCommand())
                    {
                        var param = new SQLiteParameter
                        {
                            ParameterName = "@NOME",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = value.Nome
                        };

                        cmd.Parameters.Add(param);

                        param = new SQLiteParameter
                        {
                            ParameterName = "@CNPJ",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = value.Cnpj
                        };

                        cmd.Parameters.Add(param);

                        param = new SQLiteParameter
                        {
                            ParameterName = "@CODIGO_TIPO",
                            DbType = DbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = value.CodigoTipo
                        };

                        cmd.Parameters.Add(param);

                        param = new SQLiteParameter
                        {
                            ParameterName = "@CODIGO",
                            DbType = DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = value.Codigo
                        };

                        cmd.Parameters.Add(param);

                        cmd.CommandText = "UPDATE FUNDO SET Nome = @NOME, CNPJ = @CNPJ, CODIGO_TIPO = @CODIGO_TIPO WHERE CODIGO = @CODIGO";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conexao;

                        var success = await cmd.ExecuteNonQueryAsync();
                        if (success > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
