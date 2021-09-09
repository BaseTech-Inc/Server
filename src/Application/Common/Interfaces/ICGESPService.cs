using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
