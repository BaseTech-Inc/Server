using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Estados.Queries.GetEstadosByName;
using Domain.Entities;

namespace Application.Estados.Queries.GetPaisesByName
{
    public class GetEstadosByNameQuery
    {
        public string name { get; set; }
    }

    public class GetEstadosByNameQueryHandler : IGetEstadosByNameQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetEstadosByNameQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Estado>> Handle(GetEstadosByNameQuery request)
        {
            try
            {
                var entity = _context.Estado.Where(x => EF.Functions.Like(x.Nome, "%" + request.name + "%")).Include(e => e.Pais).ToList();

                return new Response<IList<Estado>>(data: entity);
            }
            catch
            {
                return new Response<IList<Estado>>(message: $"error to get");
            }
        }
    }
}
