using CaseItau.API.Infrastructure.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Service.Fundo.Queries.GetAll
{
    public class FundoGetAllQueryHandler : IRequestHandler<FundoGetAllQueryRequest, IEnumerable<FundoGetAllQueryResponse>>
    {
        private readonly IitauDbContext Context;
        public FundoGetAllQueryHandler(IitauDbContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<FundoGetAllQueryResponse>> Handle(FundoGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            return await SearchFundos();
        }

        private async Task<IEnumerable<FundoGetAllQueryResponse>> SearchFundos()
        {
            return await Context.Fundos
                .Include(e => e.TipoFundo)
                .Select(e =>
             new FundoGetAllQueryResponse
             {
                 Codigo = e.Codigo,
                 Cnpj = e.Cnpj,
                 CodigoTipo = e.TipoFundoCodigo,
                 Nome = e.Nome,
                 Patrimonio = e.Patrimonio,
                 NomeTipo = e.TipoFundo.Nome
             }).ToListAsync();
        }
    }
}
