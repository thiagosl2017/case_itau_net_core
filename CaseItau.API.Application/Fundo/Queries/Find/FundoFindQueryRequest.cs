using MediatR;

namespace CaseItau.API.Service.Fundo.Queries.Find
{
    public class FundoFindQueryRequest : IRequest<FundoFindQueryResponse>
    {
        public string Codigo { get; set; }
    }
}
