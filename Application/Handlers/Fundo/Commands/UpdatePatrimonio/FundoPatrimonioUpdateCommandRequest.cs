using MediatR;

namespace CaseItau.API.Application.Handler.Fundo.Commands.UpdatePatrimonio
{
    public class FundoPatrimonioUpdateCommandRequest : IRequest<FundoPatrimonioUpdateCommandResponse>
    {
        public string Codigo { get; set; }
        public decimal Patrimonio { get; set; }
    }
}
