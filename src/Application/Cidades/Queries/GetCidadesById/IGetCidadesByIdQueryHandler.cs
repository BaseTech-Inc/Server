using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cidades.Queries.GetCidadesById
{
    public interface IGetCidadesByIdQueryHandler
    {
        Response<Cidade> Handle(GetCidadesByIdQuery request);
    }
}
