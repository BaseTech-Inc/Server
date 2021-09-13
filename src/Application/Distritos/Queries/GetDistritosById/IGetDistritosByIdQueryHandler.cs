using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Distritos.Queries.GetDistritosById
{
    public interface IGetDistritosByIdQueryHandler
    {
        Response<Distrito> Handle(GetDistritosByIdQuery request);
    }
}
