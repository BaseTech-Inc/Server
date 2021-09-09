using Application.Alertas.Queries.GetAlertasByDate;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using HtmlAgilityPack;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var htmlText = await ProcessHttpCGESP(date);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlText);

            var documentContent = htmlDoc.DocumentNode.querySelector(".content");
            var childNodesContent = documentContent.ChildNodes;

            var listAlertas = new List<Alerta>();

            foreach (var node in childNodesContent)
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    // div.content > h1.tit-bairros
                    if (node.HasClass("tit-bairros"))
                    {
                        // Zonas
                        // Nome zona
                    }
                    // div.content > table.tb-pontos-de-alagamentos
                    else if (node.HasClass("tb-pontos-de-alagamentos"))
                    {
                        // Zonas > Bairros
                        var RowsTable = node.ChildNodes;
                        var bairro = "";

                        // div.content > table.tb-pontos-de-alagamentos > tbody
                        foreach (var RowTable in RowsTable)
                        {
                            var CellsTable = RowTable.ChildNodes;

                            // div.content > table.tb-pontos-de-alagamentos > tbody > tr
                            foreach (var CellTable in CellsTable)
                            {
                                // div.content > table.tb-pontos-de-alagamentos > tbody > tr > td.bairro
                                if (CellTable.HasClass("bairro"))
                                {
                                    // Nome bairro
                                    bairro = CellTable.InnerText.Trim();
                                }
                                // div.content > table.tb-pontos-de-alagamentos > tbody > tr > td.total-pts
                                else if (CellTable.HasClass("total-pts"))
                                {
                                    // Total de Pontos
                                }
                                // div.content > table.tb-pontos-de-alagamentos > tbody > tr > td
                                else if (CellTable.ChildNodes.Count > 0)
                                {
                                    // Zonas > Bairros > Enchentes
                                    var alertas = new Alerta();

                                    var bairroList = bairro;

                                    if (bairro.Contains("/"))
                                        bairroList = bairro.Split("/ ")[0];

                                    var distrito = _context.Distrito
                                        .Where(x => EF.Functions.Like(x.Nome, "%" + bairroList + "%"))
                                            .Where(x => EF.Functions.Like(x.Cidade.Nome, "São Paulo"))
                                                .Where(x => EF.Functions.Like(x.Cidade.Estado.Nome, "São Paulo"))
                                                    .Where(x => EF.Functions.Like(x.Cidade.Estado.Pais.Nome, "Brasil"))
                                                        .Include(e => e.Cidade)
                                                            .Include(e => e.Cidade.Estado)
                                                                .Include(e => e.Cidade.Estado.Pais)
                                                                    .OrderBy(x => x.Nome)
                                                                        .ToList()
                                                                            .FirstOrDefault();

                                    alertas.Distrito = distrito;

                                    var infosEnchente = CellTable.ChildNodes[1].ChildNodes[1].ChildNodes;

                                    // div.content > table.tb-pontos-de-alagamentos > tr > td > div > ul > li
                                    foreach (var infoEnchentes in infosEnchente)
                                    {
                                        // Zonas > Bairros > Enchentes > Informacoes
                                        if (infoEnchentes.Name == "li")
                                        {
                                            // div.content > table.tb-pontos-de-alagamentos > tbody > tr > td > div > ul > li.inativo-intransitavel
                                            if (infoEnchentes.HasClass("inativo-intransitavel"))
                                            {
                                                alertas.Transitividade = false;
                                            }
                                            // div.content > table.tb-pontos-de-alagamentos > tr > td > div > ul > li.inativo-transitavel
                                            else if (infoEnchentes.HasClass("inativo-transitavel"))
                                            {
                                                alertas.Transitividade = true;
                                            }
                                            // div.content > table.tb-pontos-de-alagamentos > tbody > tr > td > div > ul > li.col-local
                                            else if (infoEnchentes.HasClass("col-local"))
                                            {
                                                var horario = infoEnchentes.ChildNodes[0].InnerText.Trim();

                                                var found = horario.IndexOf("De");
                                                var horarios = horario.Substring(found + 3).Split(" a ");

                                                var tempoInicio = new DateTime(
                                                    date.Year,
                                                    date.Month,
                                                    date.Day,
                                                    Int32.Parse(horarios[0].Split(":")[0]),
                                                    Int32.Parse(horarios[0].Split(":")[1]),
                                                    0);

                                                alertas.TempoInicio = tempoInicio;

                                                var tempoFinal = new DateTime(
                                                    date.Year,
                                                    date.Month,
                                                    date.Day,
                                                    Int32.Parse(horarios[1].Split(":")[0]),
                                                    Int32.Parse(horarios[1].Split(":")[1]),
                                                    0);

                                                alertas.TempoFinal = tempoFinal;

                                                var nominatimService = new NominatimService();

                                                var local = infoEnchentes.ChildNodes[2].InnerText.Trim().AllFirstCharToUpper();

                                                var nominatimResult = await nominatimService.ProcessNominatim(
                                                    local,
                                                    alertas.Distrito.Nome,
                                                    "São Paulo",
                                                    "São Paulo");

                                                if (nominatimResult.Count > 0)
                                                {
                                                    var lat = double.Parse(
                                                    nominatimResult[0].lat,
                                                    CultureInfo.InvariantCulture);
                                                    var lon = double.Parse(
                                                        nominatimResult[0].lon,
                                                        CultureInfo.InvariantCulture);

                                                    var ponto = new Ponto()
                                                    {
                                                        Latitude = lat,
                                                        Longitude = lon
                                                    };

                                                    alertas.Ponto = ponto;
                                                }
                                                
                                            }
                                            // div.content > table.tb-pontos-de-alagamentos > tbody > tr > td > div > ul > li.arial-descr-alag
                                            else if (infoEnchentes.HasClass("arial-descr-alag"))
                                            {
                                                var descricao = infoEnchentes.ChildNodes[0].InnerText.Trim().Split("Sentido: ")[1].AllFirstCharToUpper() + " " + infoEnchentes.ChildNodes[2].InnerText.Trim().Split("Referência: ")[1].AllFirstCharToUpper();

                                                alertas.Descricao = descricao;
                                            }
                                        }
                                    }

                                    listAlertas.Add(alertas);
                                }
                            }
                        }
                    }
                }
            }

            var request = new GetAlertasByDateQuery 
            {
                date = date
            };

            var listAlertasDb = _context.Alerta
                    .Where(x => x.TempoInicio.Date == request.date.Date)
                        .Include(i => i.Ponto)
                            .Include(e => e.Distrito)
                                .Include(e => e.Distrito.Cidade.Estado)
                                    .Include(e => e.Distrito.Cidade.Estado.Pais)
                                        .ToList();

            listAlertas.AddRange(listAlertasDb);

            return new Response<IList<Alerta>>(listAlertas, message: $"");
        }

        public async Task<Response<IList<Alerta>>> ProcessCGESPByDistrict(
            DateTime date, 
            string district, 
            string city = "São Paulo", 
            string state = "São Paulo",
            string country = "Brasil")
        {
            var result = await ProcessCGESPByDate(date);
            var listResult = new List<Alerta>();

            if (result.Succeeded)
            {
                var listAlertsResult = result.Data;

                foreach (var alertResult in listAlertsResult)
                {
                    var distritos = _context.Distrito
                        .Where(x => EF.Functions.Like(x.Nome, "%" + district + "%"))
                            .Where(x => EF.Functions.Like(x.Cidade.Nome, "%" + city + "%"))
                                .Where(x => EF.Functions.Like(x.Cidade.Estado.Nome, "%" + state + "%"))
                                    .Where(x => EF.Functions.Like(x.Cidade.Estado.Pais.Nome, "%" + country + "%"))
                                        .OrderBy(x => x.Nome)
                                            .ToList();

                    foreach (var distrito in distritos)
                    {
                        if (distrito.Nome == alertResult.Distrito.Nome)
                            listResult.Add(alertResult);
                    }
                }
            }

            return new Response<IList<Alerta>>(listResult, message: $"");
        }
    }
}
