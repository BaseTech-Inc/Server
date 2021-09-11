using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Distritos.Queries.GetDistritosByName
{
    public interface IGetDistritosByNameQueryHandler
    {
        Response<IList<Distrito>> Handle(GetDistritosByNameQuery request);
    }
}
