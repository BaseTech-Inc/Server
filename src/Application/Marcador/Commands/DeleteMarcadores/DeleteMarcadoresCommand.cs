using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Marcador.Commands.UpdateMarcadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Commands.DeleteMarcadores
{
    public class DeleteMarcadoresCommand
    {
        public string Id { get; set; }

        public string UserId { get; set; }
    }

    public class DeleteMarcadoresCommandHandler : IDeleteMarcadoresCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public DeleteMarcadoresCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(DeleteMarcadoresCommand request)
        {
            var entityUsuario = _context.Usuario.Where(x => x.Id == request.UserId).FirstOrDefault();

            var entity = _context.Marcadores.Where(x => x.Id == request.Id).Where(x => x.Usuario == entityUsuario).FirstOrDefault();

            try
            {
                _context.Marcadores.Remove(entity);

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
