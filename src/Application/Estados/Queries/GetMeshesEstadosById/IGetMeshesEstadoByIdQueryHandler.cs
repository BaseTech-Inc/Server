using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Estados.Queries.GetMeshesEstadosById
{
    public interface IGetMeshesEstadoByIdQueryHandler
    {
        Response<EstadoVm> Handle(GetMeshesEstadoByIdQuery request);
    }
}
