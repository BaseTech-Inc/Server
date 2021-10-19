using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetMaisPesquisadosWithPagination
{
    public class MaisPesquisadosDto
    {
        public Distrito Distrito { get; set; }

        public int Count { get; set; }
    }
}
