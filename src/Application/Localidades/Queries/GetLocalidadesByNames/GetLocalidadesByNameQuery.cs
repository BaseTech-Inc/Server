using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Localidades.Queries.GetLocalidadesByNames
{
    public class GetLocalidadesByNameQuery
    {
        public string namePais { get; set; }
        public string nameEstado { get; set; }
        public string nameCidade { get; set; }
        public string nameDistrito { get; set; }
    }

    public class GetLocalidadeByNameQueryHandler : IGetLocalidadeByNameQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetLocalidadeByNameQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Distrito>> Handle(GetLocalidadesByNameQuery request)
        {
            try
            {
                var entity = _context.Distrito
                    .Where(x => EF.Functions.Like(x.Nome, "%" + request.nameDistrito + "%"))
                        .Where(x => EF.Functions.Like(x.Cidade.Nome, "%" + request.nameCidade + "%"))
                            .Where(x => EF.Functions.Like(x.Cidade.Estado.Nome, "%" + request.nameEstado + "%"))
                                .Where(x => EF.Functions.Like(x.Cidade.Estado.Pais.Nome, "%" + request.namePais + "%"))
                                    .Include(e => e.Cidade)
                                        .Include(e => e.Cidade.Estado)
                                            .Include(e => e.Cidade.Estado.Pais)
                                                .ToList();

                return new Response<IList<Distrito>>(data: entity);
            }
            catch
            {
                return new Response<IList<Distrito>>(message: $"erro para obter");
            }
        }
    }
}
