using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace Application.Distritos.Queries.GetAllDistritos
{
    public class GetAllDistritosQuery
    { }

    public class GetAllDistritosQueryHandler : IGetAllDistritosQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetAllDistritosQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Distrito>> Handle(GetAllDistritosQuery request)
        {
            try
            {
                return new Response<IList<Distrito>>(_context.Distrito.Include(e => e.Cidade).Include(e => e.Cidade.Estado).Include(e => e.Cidade.Estado.Pais).ToList());
            }
            catch
            {
                return new Response<IList<Distrito>>(message: $"error to get");
            }
        }
    }
}
