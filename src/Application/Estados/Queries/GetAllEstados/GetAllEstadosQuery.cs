using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Estados.Queries.GetAllEstados
{
    public class GetAllEstadosQuery
    { }

    public class GetAllEstadosQueryHandler : IGetAllEstadosQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetAllEstadosQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Estado>> Handle(GetAllEstadosQuery request)
        {
            try
            {
                return new Response<IList<Estado>>(_context.Estado.Include(e => e.Pais).ToList());
            }
            catch
            {
                return new Response<IList<Estado>>(message: $"error to get");
            }
        }
    }
}
