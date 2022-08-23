using MediatR;
using System.Collections.Generic;

namespace CaseItau.API.Service.Fundo.Queries.GetAll
{
    public class FundoGetAllQueryRequest : IRequest<IEnumerable<FundoGetAllQueryResponse>>
    {
    }
}
