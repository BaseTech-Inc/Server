using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Queries.GetAllMarcadores
{
    public interface IGetAllMarcadoresQueryHandler
    {
        Response<IList<Marcadores>> Handle(GetAllMarcadoresQuery request);
    }
}
