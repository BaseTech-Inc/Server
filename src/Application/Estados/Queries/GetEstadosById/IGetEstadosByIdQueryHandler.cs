using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Estados.Queries.GetEstadosById
{
    public interface IGetEstadosByIdQueryHandler
    {
        Response<Estado> Handle(GetEstadosByIdQuery request);
    }
}
