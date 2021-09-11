using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Paises.Queries.GetAllPaises
{
    public interface IGetAllPaisesQueryHandler
    {
        Response<IList<Pais>> Handle(GetAllPaisesQuery request);
    }
}
