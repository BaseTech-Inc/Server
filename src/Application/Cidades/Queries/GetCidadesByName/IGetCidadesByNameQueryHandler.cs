using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cidades.Queries.GetCidadesByName
{
    public interface IGetCidadesByNameQueryHandler
    {
        Response<IList<Cidade>> Handle(GetCidadesByNameQuery request);
    }
}
