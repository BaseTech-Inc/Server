using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cidades.Queries.GetMeshesCidadeById
{
    public interface IGetMeshesCidadesByIdQueryHandler
    {
        Response<CidadeVm> Handle(GetMeshesCidadesByIdQuery request);
    }
}
