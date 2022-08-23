using CaseItau.API.Domain.Common.Interfaces;
using CaseItau.API.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Application.Handler.Fundo.Commands.Delete
{
    public class FundoDeleteCommandHandler : IRequestHandler<FundoDeleteCommandRequest, FundoDeleteCommandResponse>
    {
        private readonly IitauDbContext Context;
        public FundoDeleteCommandHandler(IitauDbContext context)
        {
            Context = context;
        }
        public async Task<FundoDeleteCommandResponse> Handle(FundoDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var resultDelete = await DeleteFundo(request, cancellationToken);
            return CreateResponse(resultDelete);
        }

        private async Task<Domain.Models.Fundo> SearchFundo(FundoDeleteCommandRequest request)
        {
            var fundo = await Context.Fundos.Where(e => e.Codigo == request.Codigo).FirstOrDefaultAsync();
            if (fundo == null)
                throw new NotFoundException();
            return fundo;
        }

        private FundoDeleteCommandResponse CreateResponse(int resultDelete)
        {
            return new FundoDeleteCommandResponse
            {
                Success = (resultDelete > 0)
            };
        }

        private async Task<int> DeleteFundo(FundoDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var fundo = await SearchFundo(request);
            Context.Fundos.Remove(fundo);
            var result = await Context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
