using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Marcador.Queries.GetAllMarcadores;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Paises.Queries.GetAllPaises
{
    public class GetAllPaisesQuery
    { }

    public class GetAllPaisesQueryHandler : IGetAllPaisesQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetAllPaisesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Pais>> Handle(GetAllPaisesQuery request)
        {
            try
            {
                return new Response<IList<Pais>>(data: _context.Pais.ToList());
            }
            catch
            {
                return new Response<IList<Pais>>(message: $"error to get");
            }
        }
    }
}
