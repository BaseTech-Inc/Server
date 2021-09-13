using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Paises.Queries.GetPaisesById
{
    public class GetPaisesByIdQuery
    {
        public string Id { get; set; }
    }

    public class GetPaisesByIdQueryHandler : IGetPaisesByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetPaisesByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<Pais> Handle(GetPaisesByIdQuery request)
        {
            try
            {
                var entity = _context.Pais
                    .Where(x => x.Id == request.Id)
                        .ToList()
                            .FirstOrDefault();

                return new Response<Pais>(data: entity);
            }
            catch
            {
                return new Response<Pais>(message: $"error to get");
            }
        }
    }
}
