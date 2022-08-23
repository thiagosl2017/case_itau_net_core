using CaseItau.API.Domain.Common.Interfaces;
using CaseItau.API.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Application.Handler.Fundo.Queries.Find
{
    public class FundoFindQueryHandler : IRequestHandler<FundoFindQueryRequest, FundoFindQueryResponse>
    {
        private readonly IitauDbContext Context;
        public FundoFindQueryHandler(IitauDbContext context)
        {
            Context = context;
        }
        public async Task<FundoFindQueryResponse> Handle(FundoFindQueryRequest request, CancellationToken cancellationToken)
        {
            return await SearchFundo(request);
        }

        private async Task<FundoFindQueryResponse> SearchFundo(FundoFindQueryRequest request)
        {
            var fundo = await Context.Fundos
                .Where(e => e.Codigo == request.Codigo)
                .Include(e => e.TipoFundo)
                .Select(e =>
             new FundoFindQueryResponse
             {
                 Codigo = request.Codigo,
                 Cnpj = e.Cnpj,
                 CodigoTipo = e.TipoFundoCodigo,
                 Nome = e.Nome,
                 Patrimonio = e.Patrimonio,
                 NomeTipo = e.TipoFundo.Nome
             }).FirstOrDefaultAsync();

            if (fundo == null)
                throw new NotFoundException();

            return fundo;
        }
    }
}
