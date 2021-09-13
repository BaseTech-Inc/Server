using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Paises.Queries.GetPaisesById
{
    public interface IGetPaisesByIdQueryHandler
    {
        Response<Pais> Handle(GetPaisesByIdQuery request);
    }
}
