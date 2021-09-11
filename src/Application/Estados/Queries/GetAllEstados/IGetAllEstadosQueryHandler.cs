using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Estados.Queries.GetAllEstados
{
    public interface IGetAllEstadosQueryHandler
    {
        Response<IList<Estado>> Handle(GetAllEstadosQuery request);
    }
}
