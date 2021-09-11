using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Cidades.Queries.GetCidadesByName
{
    public interface IGetCidadesByNameQueryHandler
    {
        Response<IList<Cidade>> Handle(GetCidadesByNameQuery request);
    }
}
