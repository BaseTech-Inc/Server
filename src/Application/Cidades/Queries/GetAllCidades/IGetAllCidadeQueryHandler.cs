using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Cidades.Queries.GetAllCidades
{
    public interface IGetAllCidadeQueryHandler
    {
        Response<IList<Cidade>> Handle(GetAllCidadeQuery request);
    }
}
