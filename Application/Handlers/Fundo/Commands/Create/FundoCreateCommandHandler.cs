using CaseItau.API.Domain.Common.Interfaces;
using CaseItau.API.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Application.Handler.Fundo.Commands.Create
{
    public class FundoCreateCommandHandler : IRequestHandler<FundoCreateCommandRequest, FundoCreateCommandResponse>
    {
        private readonly IitauDbContext Context;
        public FundoCreateCommandHandler(IitauDbContext context)
        {
            Context = context;
        }
        public async Task<FundoCreateCommandResponse> Handle(FundoCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var nameTipoFundo = await SearchNameTipoFundos(request);
            var newFundo = await AddFundo(request, cancellationToken);
            return CreateResponse(newFundo, nameTipoFundo);
        }

        private async Task<string> SearchNameTipoFundos(FundoCreateCommandRequest request)
        {
            var tipoFundo = await Context.TipoFundos.Where(e => e.Codigo == request.CodigoTipo).FirstOrDefaultAsync();
            if (tipoFundo == null)
                throw new NotFoundException();
            return tipoFundo.Nome;
        }

        private FundoCreateCommandResponse CreateResponse(Domain.Models.Fundo newFundo, string nameTipoFundo)
        {
            return new FundoCreateCommandResponse
            {
                Cnpj = newFundo.Cnpj,
                Codigo = newFundo.Codigo,
                Nome = newFundo.Nome,
                Patrimonio = newFundo.Patrimonio,
                CodigoTipo = newFundo.TipoFundoCodigo,
                NomeTipo = nameTipoFundo
            };
        }

        private async Task<Domain.Models.Fundo> AddFundo(FundoCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var createFundo = new CaseItau.API.Domain.Models.Fundo()
            {
                Codigo = request.Codigo,
                Cnpj = request.Cnpj,
                Nome = request.Nome,
                Patrimonio = request.Patrimonio,
                TipoFundoCodigo = request.CodigoTipo
            };
            Context.Fundos.Add(createFundo);
            await Context.SaveChangesAsync(cancellationToken);
            return createFundo;
        }
    }
}
