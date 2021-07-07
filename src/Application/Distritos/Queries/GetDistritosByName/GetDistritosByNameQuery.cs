using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Paises.Queries.GetPaisesByName;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Distritos.Queries.GetDistritosByName
{
    public class GetDistritosByNameQuery
    {
        public string name { get; set; }
    }

    public class GetDistritosByNameQueryHandler : IGetDistritosByNameQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetDistritosByNameQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Distrito>> Handle(GetDistritosByNameQuery request)
        {
            try
            {
                var entity = _context.Distrito.Where(x => EF.Functions.Like(x.Nome, "%" + request.name + "%")).Include(e => e.Cidade).Include(e => e.Cidade.Estado).Include(e => e.Cidade.Estado.Pais).ToList();

                return new Response<IList<Distrito>>(data: entity);
            }
            catch
            {
                return new Response<IList<Distrito>>(message: $"error to get");
            }
        }
    }
}
