using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Cidades.Queries.GetAllCidades
{
    public class GetAllCidadeQuery
    { }

    public class GetAllCidadeQueryHandler : IGetAllCidadeQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetAllCidadeQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Cidade>> Handle(GetAllCidadeQuery request)
        {
            try
            {
                return new Response<IList<Cidade>>(_context.Cidade.Include(e => e.Estado).Include(e => e.Estado.Pais).ToList());
            }
            catch
            {
                return new Response<IList<Cidade>>(message: $"erro para obter");
            }
        }
    }
}
