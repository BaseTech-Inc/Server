using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Alertas.Queries.GetAlertasByDate
{
    public interface IGetAlertasByDateQueryHandler
    {
        Response<IList<Alerta>> Handle(GetAlertasByDateQuery request);
    }
}
