using Application.Common.Interfaces;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Alertas.Commands.DeleteAlertas
{
    public class DeleteAlertasCommand
    {
        public string Id { get; set; }
    }

    public class DeleteAlertasCommandHandler : IDeleteAlertasCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public DeleteAlertasCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(DeleteAlertasCommand request)
        {
            var entity = _context.Alerta
                .Where(x => x.Id == request.Id)
                    .FirstOrDefault();

            try
            {
                _context.Alerta.Remove(entity);

                _context.SaveChanges();

                return new Response<string>(data: entity.Id);
            }
            catch
            {
                return new Response<string>(message: $"erro para apagar: ${ entity.Id }");
            }
        }
    }
}
