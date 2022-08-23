using MediatR;

namespace CaseItau.API.Service.Fundo.Commands.UpdatePatrimonio
{
    public class FundoPatrimonioUpdateCommandRequest : IRequest<FundoPatrimonioUpdateCommandResponse>
    {
        public double Patrimonio { get; set; }
    }
}
