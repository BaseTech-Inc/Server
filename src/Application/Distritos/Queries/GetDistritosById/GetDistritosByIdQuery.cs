using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Distritos.Queries.GetDistritosById
{
    public class GetDistritosByIdQuery
    {
        public string Id { get; set; }
    }

    public class GetDistritosByIdQueryHandler : IGetDistritosByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetDistritosByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<Distrito> Handle(GetDistritosByIdQuery request)
        {
            try
            {
                var entity = _context.Distrito
                    .Where(x => x.Id == request.Id)
                        .Include(e => e.Cidade)
                            .Include(e => e.Cidade.Estado)
                                .Include(e => e.Cidade.Estado.Pais)
                                    .ToList()
                                        .FirstOrDefault();

                return new Response<Distrito>(data: entity);
            }
            catch
            {
                return new Response<Distrito>(message: $"erro para obter");
            }
        }
    }
}
