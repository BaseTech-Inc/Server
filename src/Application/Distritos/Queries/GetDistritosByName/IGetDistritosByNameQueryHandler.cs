using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Distritos.Queries.GetDistritosByName
{
    public interface IGetDistritosByNameQueryHandler
    {
        Response<IList<Distrito>> Handle(GetDistritosByNameQuery request);
    }
}
