using MediatR;

namespace CaseItau.API.Application.Handler.Fundo.Commands.Delete
{
    public class FundoDeleteCommandRequest : IRequest<FundoDeleteCommandResponse>
    {
        public string Codigo { get; set; }
    }
}
