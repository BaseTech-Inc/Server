using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Paises.Queries.GetMeshesPaisesById
{
    public interface IGetMeshesPaisesByIdQueryHandler
    {
        Response<PaisesVm> Handle(GetMeshesPaisesByIdQuery request);
    }
}
