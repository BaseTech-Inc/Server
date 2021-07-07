using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Estados.Queries.GetAllEstados
{
    public interface IGetAllEstadosQueryHandler
    {
        Response<IList<Estado>> Handle(GetAllEstadosQuery request);
    }
}
