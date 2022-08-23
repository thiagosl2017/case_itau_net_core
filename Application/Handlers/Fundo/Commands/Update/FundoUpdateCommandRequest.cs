using MediatR;

namespace CaseItau.API.Application.Handler.Fundo.Commands.Update
{
    public class FundoUpdateCommandRequest : IRequest<FundoUpdateCommandResponse>
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int CodigoTipo { get; set; }
    }
}
