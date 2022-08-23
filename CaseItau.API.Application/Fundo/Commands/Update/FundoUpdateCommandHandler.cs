using CaseItau.API.Infrastructure.Persistence.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Service.Fundo.Commands.Update
{
    public class FundoUpdateCommandHandler : IRequestHandler<FundoUpdateCommandRequest, FundoUpdateCommandResponse>
    {
        private readonly IitauDbContext Context;
        public FundoUpdateCommandHandler(IitauDbContext context)
        {
            Context = context;
        }
        public Task<FundoUpdateCommandResponse> Handle(FundoUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void update()
        {/*
            var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE FUNDO SET Nome = '" + value.Nome + "', CNPJ = '" + value.Cnpj + "', CODIGO_TIPO = " + value.CodigoTipo + " WHERE CODIGO = '" + codigo + "'";
            cmd.CommandType = System.Data.CommandType.Text;
            var resultado = cmd.ExecuteNonQuery();*/
        }

        private void patrimonio()
        {/*
            var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE FUNDO SET PATRIMONIO = IFNULL(PATRIMONIO,0) + " + value.ToString() + " WHERE CODIGO = '" + codigo + "'";
            cmd.CommandType = System.Data.CommandType.Text;
            var resultado = cmd.ExecuteNonQuery();*/
        }
    }
}
