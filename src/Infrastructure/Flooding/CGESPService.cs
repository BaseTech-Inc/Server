using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using HtmlAgilityPack;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Flooding
{
    public class CGESPService : ICGESPService
    {
        private readonly IApplicationDbContext _context;

        public CGESPService(
            IApplicationDbContext context) 
        {
            _context = context;
        }

        private static readonly string baseUrl = "https://www.cgesp.org/v3/alagamentos.jsp";

        private async Task<string> ProcessHttpCGESP(DateTime date)
        {
            var day = date.Day;
            var month = date.Month;
            var year = date.Year;

            string url = baseUrl
                .SetQueryParams(new
                {
                    dataBusca = $"{day}/{month}/{year}",
                    enviaBusca = "Busca"
                });


            var htmlText = await HttpRequestUrl.ProcessHttpClient(url, mediaType: "text/html");

            return htmlText;
        }

        public async Task<Response<IList<Alerta>>> ProcessCGESPByDate(DateTime date)
        {
            // Get htmlText
            var htmlText = await ProcessHttpCGESP(date);

            // Convert to html element
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlText);

            var documentContent = htmlDoc.DocumentNode.Descendants(0)
                .Where(n => n.HasClass("col-alagamentos"))
                        .FirstOrDefault()
                            .Descendants(0)
                                .Where(n => n.HasClass("content"))
                                    .FirstOrDefault();

            var childNodes = documentContent.ChildNodes;

            var listAlerts = new List<Alerta>();
            var alerta = new Alerta();

            foreach (var node in childNodes)
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    if (node.HasClass("tit-bairros"))
                    {                        
                        // Zonas
                    }
                    else if (node.HasClass("tb-pontos-de-alagamentos"))
                    {
                        // Zonas > Bairros

                        var columns = node.ChildNodes;

                        foreach (var column in columns)
                        {
                            var lines = column.ChildNodes;

                            foreach (var line in lines)
                            {
                                if (line.HasClass("bairro"))
                                {
                                    alerta = new Alerta();

                                    var bairro = line.InnerText.Trim().Split("/ ")[0];

                                    var distrito = _context.Distrito
                                        .Where(x => EF.Functions.Like(x.Nome, "%" + bairro + "%"))
                                            .Where(x => EF.Functions.Like(x.Cidade.Nome, "São Paulo"))
                                                .Where(x => EF.Functions.Like(x.Cidade.Estado.Nome, "São Paulo"))
                                                    .Where(x => EF.Functions.Like(x.Cidade.Estado.Pais.Nome, "Brasil"))
                                                        .Include(e => e.Cidade)
                                                            .Include(e => e.Cidade.Estado)
                                                                .Include(e => e.Cidade.Estado.Pais)
                                                                    .ToList()
                                                                        .FirstOrDefault();

                                    alerta.Distrito = distrito;
                                }
                                else if (line.HasClass("total-pts"))
                                {
                                    // bairroEntity.pontos = line.InnerText.Trim();
                                }
                                else if (line.ChildNodes.Count > 0)
                                {
                                    // Zonas > Bairros > Enchentes

                                    var infosEnchente = line.ChildNodes[1].ChildNodes[1].ChildNodes;

                                    foreach (var infoEnchentes in infosEnchente)
                                    {
                                        // Zonas > Bairros > Enchentes > Informacoes
                                        if (infoEnchentes.Name == "li")
                                        {
                                            if (infoEnchentes.HasClass("inativo-intransitavel"))
                                            {
                                                alerta.Transitividade = false;
                                            }
                                            else if (infoEnchentes.HasClass("inativo-transitavel"))
                                            {
                                                alerta.Transitividade = true;
                                            }
                                            else if (infoEnchentes.HasClass("col-local"))
                                            {
                                                alerta.Tempo = date;
                                                alerta.Descricao = infoEnchentes.ChildNodes[2].InnerText.Trim().AllFirstCharToUpper();

                                                //enchentesEntity.horario = infoEnchentes.ChildNodes[0].InnerText.Trim();
                                                //enchentesEntity.local = infoEnchentes.ChildNodes[2].InnerText.Trim().AllFirstCharToUpper();
                                            }
                                            else if (infoEnchentes.HasClass("arial-descr-alag"))
                                            {
                                                alerta.Descricao += infoEnchentes.ChildNodes[0].InnerText.Trim().Split("Sentido: ")[1].AllFirstCharToUpper() + infoEnchentes.ChildNodes[2].InnerText.Trim().Split("Referência: ")[1].AllFirstCharToUpper();

                                                //enchentesEntity.sentido = infoEnchentes.ChildNodes[0].InnerText.Trim().Split("Sentido: ")[1].AllFirstCharToUpper();
                                                //enchentesEntity.referencia = infoEnchentes.ChildNodes[2].InnerText.Trim().Split("Referência: ")[1].AllFirstCharToUpper();

                                                listAlerts.Add(alerta);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new Response<IList<Alerta>>(listAlerts, message: $"");
        }
    }
}
