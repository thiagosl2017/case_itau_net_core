using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Service.Fundo.Commands.UpdatePatrimonio
{
    public class FundoPatrimonioUpdateCommandHandler : IRequestHandler<FundoPatrimonioUpdateCommandRequest, FundoPatrimonioUpdateCommandResponse>
    {
        public FundoPatrimonioUpdateCommandHandler()
        {

        }
        public Task<FundoPatrimonioUpdateCommandResponse> Handle(FundoPatrimonioUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
