using Application.Common.Models;
using Application.Estados.Queries.GetPaisesByName;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Estados.Queries.GetEstadosByName
{
    public interface IGetEstadosByNameQueryHandler
    {
        Response<IList<Estado>> Handle(GetEstadosByNameQuery request);
    }
}
