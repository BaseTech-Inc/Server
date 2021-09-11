using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Marcador.Queries.GetAllMarcadores
{
    public interface IGetAllMarcadoresQueryHandler
    {
        Response<IList<Marcadores>> Handle(GetAllMarcadoresQuery request);
    }
}
