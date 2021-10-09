using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ICGESPService
    {
        Task<Response<IList<Alerta>>> ProcessCGESPByDate(DateTime date);

        Task<Response<IList<Alerta>>> ProcessCGESPByDistrict(
            DateTime date,
            string district,
            string city = "São Paulo",
            string state = "São Paulo",
            string country = "Brasil");

        Task<Response<PaginatedList<Alerta>>> ProcessCGESPWithPagination(
            DateTime date,
            int PageNumber = 1,
            int PageSize = 10);
    }
}
