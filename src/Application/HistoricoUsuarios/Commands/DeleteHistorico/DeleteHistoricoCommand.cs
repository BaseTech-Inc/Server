using Application.Common.Interfaces;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Commands.DeleteHistorico
{
    public class DeleteHistoricoCommand
    {
        public string Id { get; set; }

        public string UserId { get; set; }
    }

    public class DeleteHistoricoCommandHandler : IDeleteHistoricoCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public DeleteHistoricoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(DeleteHistoricoCommand request)
        {
            var entityUsuario = _context.Usuario.Where(x => x.Id == request.UserId).FirstOrDefault();

            var entity = _context.HistoricoUsuario.Where(x => x.Id == request.Id).Where(x => x.Usuario == entityUsuario).FirstOrDefault();

            try
            {
                _context.HistoricoUsuario.Remove(entity);

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
