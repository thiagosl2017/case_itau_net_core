using CaseItau.API.Domain.Common.Interfaces;
using CaseItau.API.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Application.Handler.Fundo.Commands.Update
{
    public class FundoUpdateCommandHandler : IRequestHandler<FundoUpdateCommandRequest, FundoUpdateCommandResponse>
    {
        private readonly IitauDbContext Context;
        public FundoUpdateCommandHandler(IitauDbContext context)
        {
            Context = context;
        }
        public async Task<FundoUpdateCommandResponse> Handle(FundoUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var nameTipo = await SearchNameTipoFundos(request);
            var resultUpdate = await UpdateFundo(request, cancellationToken);
            return CreateResponse(resultUpdate, nameTipo);
        }

        private async Task<string> SearchNameTipoFundos(FundoUpdateCommandRequest request)
        {
            var tipoFundo = await Context.TipoFundos.Where(e => e.Codigo == request.CodigoTipo).FirstOrDefaultAsync();
            if (tipoFundo == null)
                throw new NotFoundException();
            return tipoFundo.Nome;
        }

        private async Task<Domain.Models.Fundo> SearchFundo(FundoUpdateCommandRequest request)
        {
            var fundo = await Context.Fundos.Where(e => e.Codigo == request.Codigo).FirstOrDefaultAsync();
            if (fundo == null)
                throw new NotFoundException();
            return fundo;
        }

        private FundoUpdateCommandResponse CreateResponse(Domain.Models.Fundo newFundo, string nameTipoFundo)
        {
            return new FundoUpdateCommandResponse
            {
                Cnpj = newFundo.Cnpj,
                Codigo = newFundo.Codigo,
                Nome = newFundo.Nome,
                Patrimonio = newFundo.Patrimonio,
                CodigoTipo = newFundo.TipoFundoCodigo,
                NomeTipo = nameTipoFundo
            };
        }

        private async Task<Domain.Models.Fundo> UpdateFundo(FundoUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var fundo = await SearchFundo(request);
            fundo.Codigo = request.Codigo;
            fundo.Nome = request.Nome;
            fundo.Cnpj = request.Cnpj;
            fundo.TipoFundoCodigo = request.CodigoTipo;
            Context.Fundos.Update(fundo);
            await Context.SaveChangesAsync(cancellationToken);
            return fundo;
        }

    }
}
