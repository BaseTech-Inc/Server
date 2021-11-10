using System.Linq;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Marcador.Commands.UpdateMarcadores
{
    public class UpdateMarcadoresCommand
    {
        public string Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string UserId { get; set; }

        public string Nome { get; set; }
    }

    public class UpdateMarcadoresCommandHandler : IUpdateMarcadoresCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public UpdateMarcadoresCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Marcadores>> Handle(UpdateMarcadoresCommand request)
        {
            var entityUsuario = _context.Usuario.Where(x => x.Id == request.UserId).FirstOrDefault();

            var entity = _context.Marcadores.Where(x => x.Id == request.Id).Where(x => x.Usuario == entityUsuario).FirstOrDefault();

            if (entity != null)
            {
                try
                {
                    entity.Nome = request.Nome;
                    entity.Ponto = new Ponto()
                    {
                        Latitude = request.Latitude,
                        Longitude = request.Longitude
                    };

                    _context.SaveChanges();

                    return new Response<Marcadores>(data: entity);
                }
                catch
                {
                    return new Response<Marcadores>(message: $"erro para atualizar: ${ entity.Id }");
                }
            }
            else
            {
                return new Response<Marcadores>(message: $"nenhum ${ entity.Id } foi encontrado ");
            }
        }
    }
}
