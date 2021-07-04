using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AdministrativeDivision
{
    public class AdministrativeDivisionService
    {
        const string baseUrl = "https://servicodados.ibge.gov.br/api/v1/localidades";

        public async Task<List<T>> ProcessDivisions<T>(string path)
        {
            string url = baseUrl
                .AddPath(path);

            var division = await HttpRequestUrl.ProcessHttpClient<T>(url);

            return division;
        }
    }
}
