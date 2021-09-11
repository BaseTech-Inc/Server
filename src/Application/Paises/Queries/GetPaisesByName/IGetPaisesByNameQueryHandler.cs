using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Paises.Queries.GetPaisesByName
{
    public interface IGetPaisesByNameQueryHandler
    {
        Response<IList<Pais>> Handle(GetPaisesByNameQuery request);
    }
}
