using Application.Common.Interfaces;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Commands.DeletePontoRisco
{
    public class DeletePontoRiscoCommand
    {
        public string Id { get; set; }
    }

    public class DeletePontoRiscoCommandHandler : IDeletePontoRiscoCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public DeletePontoRiscoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(DeletePontoRiscoCommand request)
        {
            var entity = _context.PontoRisco
                .Where(x => x.Id == request.Id)
                    .FirstOrDefault();

            try
            {
                _context.PontoRisco.Remove(entity);

                _context.SaveChanges();

                return new Response<string>(data: entity.Id);
            }
            catch
            {
                return new Response<string>(message: $"error while deleting: ${ entity.Id }");
            }
        }
    }
}
