using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Distritos.Queries.GetAllDistritos
{
    public interface IGetAllDistritosQueryHandler
    {
        Response<IList<Distrito>> Handle(GetAllDistritosQuery request);
    }
}
