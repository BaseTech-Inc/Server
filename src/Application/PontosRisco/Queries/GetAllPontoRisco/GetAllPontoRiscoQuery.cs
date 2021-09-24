using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Queries.GetAllPontoRisco
{
    public class GetAllPontoRiscoQuery
    { }

    public class GetAllPontoRiscoQueryHandler : IGetAllPontoRiscoQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetAllPontoRiscoQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<PontoRisco>> Handle(GetAllPontoRiscoQuery request)
        {
            try
            {
                return new Response<IList<PontoRisco>>(data: _context.PontoRisco.ToList());
            }
            catch
            {
                return new Response<IList<PontoRisco>>(message: $"error to get");
            }
        }
    }
}
