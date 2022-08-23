using CaseItau.API.Infrastructure.Persistence.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Service.Fundo.Commands.Delete
{
    public class FundoDeleteCommandHandler : IRequestHandler<FundoDeleteCommandRequest, FundoDeleteCommandResponse>
    {
        private readonly IitauDbContext Context;
        public FundoDeleteCommandHandler(IitauDbContext context)
        {
            Context = context;
        }
        public Task<FundoDeleteCommandResponse> Handle(FundoDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void delete()
        {/*
            var con = new SQLiteConnection("Data Source=dbCaseItau.s3db");
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM FUNDO WHERE CODIGO = '" + codigo + "'";
            cmd.CommandType = System.Data.CommandType.Text;
            var resultado = cmd.ExecuteNonQuery();*/
        }
    }
}
