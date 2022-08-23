using MediatR;
using System.Collections.Generic;

namespace CaseItau.API.Application.Handler.Fundo.Queries.GetAll
{
    public class FundoGetAllQueryRequest : IRequest<IEnumerable<FundoGetAllQueryResponse>>
    {
    }
}
