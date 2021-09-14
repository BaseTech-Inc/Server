using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Queries.GetMarcadoresById
{
    public interface IGetMarcadoresByIdQueryHandler
    {
        Response<Marcadores> Handle(GetMarcadoresByIdQuery request);
    }
}
