using MediatR;

namespace CaseItau.API.Service.Fundo.Commands.Delete
{
    public class FundoDeleteCommandRequest : IRequest<FundoDeleteCommandResponse>
    {
        public string Codigo { get; set; }
    }
}
