using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Paises.Queries.GetPaisesByName
{
    public class GetPaisesByNameQuery
    {
        public string name { get; set; }
    }

    public class GetPaisesByNameQueryHandler : IGetPaisesByNameQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetPaisesByNameQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Pais>> Handle(GetPaisesByNameQuery request)
        {
            try
            {
                var entity = _context.Pais.Where(x => EF.Functions.Like(x.Nome, "%" + request.name + "%")).ToList();

                return new Response<IList<Pais>>(data: entity);
            }
            catch
            {
                return new Response<IList<Pais>>(message: $"error to get");
            }
        }
    }
}
