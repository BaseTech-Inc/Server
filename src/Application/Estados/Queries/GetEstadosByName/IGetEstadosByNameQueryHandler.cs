using System.Collections.Generic;

using Application.Common.Models;
using Application.Estados.Queries.GetPaisesByName;
using Domain.Entities;

namespace Application.Estados.Queries.GetEstadosByName
{
    public interface IGetEstadosByNameQueryHandler
    {
        Response<IList<Estado>> Handle(GetEstadosByNameQuery request);
    }
}
