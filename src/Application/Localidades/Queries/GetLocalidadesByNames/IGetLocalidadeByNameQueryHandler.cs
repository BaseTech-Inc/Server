using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Localidades.Queries.GetLocalidadesByNames
{
    public interface IGetLocalidadeByNameQueryHandler
    {
        Response<IList<Distrito>> Handle(GetLocalidadesByNameQuery request);
    }
}
