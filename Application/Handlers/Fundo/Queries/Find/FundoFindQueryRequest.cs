using MediatR;

namespace CaseItau.API.Application.Handler.Fundo.Queries.Find
{
    public class FundoFindQueryRequest : IRequest<FundoFindQueryResponse>
    {
        public string Codigo { get; set; }
    }
}
