using CaseItau.API.Domain.Common.Interfaces;
using CaseItau.API.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Application.Handler.Fundo.Commands.UpdatePatrimonio
{
    public class FundoPatrimonioUpdateCommandHandler : IRequestHandler<FundoPatrimonioUpdateCommandRequest, FundoPatrimonioUpdateCommandResponse>
    {
        private readonly IitauDbContext Context;
        public FundoPatrimonioUpdateCommandHandler(IitauDbContext context)
        {
            Context = context;
        }
        public async Task<FundoPatrimonioUpdateCommandResponse> Handle(FundoPatrimonioUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var resultUpdate = await UpdateFundoPatrimonio(request, cancellationToken);
            return CreateResponse(resultUpdate);
        }

        private async Task<Domain.Models.Fundo> SearchFundo(FundoPatrimonioUpdateCommandRequest request)
        {
            var fundo = await Context.Fundos
                .Where(e => e.Codigo == request.Codigo)
                .Include(e=> e.TipoFundo)
                .FirstOrDefaultAsync();
            if (fundo == null)
                throw new NotFoundException();
            return fundo;
        }

        private FundoPatrimonioUpdateCommandResponse CreateResponse(Domain.Models.Fundo newFundo)
        {
            return new FundoPatrimonioUpdateCommandResponse
            {
                Cnpj = newFundo.Cnpj,
                Codigo = newFundo.Codigo,
                Nome = newFundo.Nome,
                Patrimonio = newFundo.Patrimonio,
                CodigoTipo = newFundo.TipoFundoCodigo,
                NomeTipo = newFundo?.TipoFundo.Nome
            };
        }

        private async Task<Domain.Models.Fundo> UpdateFundoPatrimonio(FundoPatrimonioUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var fundo = await SearchFundo(request);
            fundo.Patrimonio = request.Patrimonio;
            Context.Fundos.Update(fundo);
            await Context.SaveChangesAsync(cancellationToken);
            return fundo;
        }
    }
}
