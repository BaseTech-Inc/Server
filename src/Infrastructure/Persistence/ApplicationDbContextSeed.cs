using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static Infrastructure.AdministrativeDivision.DivisionLocation;
using Application.Common.Enumerations;
using Application.Common.Interfaces;
using Application.GeoJson;
using Application.GeoJson.Features;
using Application.GeoJson.Geometry;
using Domain.Entities;
using Domain.Enumerations;
using Infrastructure.Identity;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var managerRole = new IdentityRole(Roles.Manager.ToString());

            if (roleManager.Roles.All(r => r.Name != managerRole.Name))
            {
                await roleManager.CreateAsync(managerRole);
            }

            var employeeRole = new IdentityRole(Roles.Employee.ToString());

            if (roleManager.Roles.All(r => r.Name != employeeRole.Name))
            {
                await roleManager.CreateAsync(employeeRole);
            }

            var listUsuario = new List<Usuario>();

            var usuario = new Usuario
            {
                TipoUsuario = new TipoUsuario
                {
                    Descricao = EnumTipoUsuario.Premium
                }
            };

            listUsuario.Add(usuario);

            var manager = new ApplicationUser { 
                UserName = "manager@localhost", 
                Email = "manager@localhost",
                Usuario = listUsuario
            };

            if (
                userManager.Users.All(u => u.UserName != manager.UserName) &&
                userManager.Users.All(u => u.Email != manager.Email))
            {
                await userManager.CreateAsync(manager, "P@assw0rd");
                await userManager.AddToRolesAsync(manager, new[] { managerRole.Name });

                var token = await userManager.GenerateEmailConfirmationTokenAsync(manager);
                await userManager.ConfirmEmailAsync(manager, token);
            }
        }        

        public static async Task SeedDefaultPlacesAsync(
            ApplicationDbContext context,
            IPlacesService placesService,
            IMeshesService meshesService,
            ILogger logger)
        {
            // Pais
            if (!context.Pais.Any())
            {
                logger.LogInformation("Pais Seed");

                var entity = new Pais
                {
                    Nome = "Brasil",
                    Sigla = "BR"
                };

                context.Pais.Add(entity);
                await context.SaveChangesAsync();

                var polygons = await SeedDefaultMeshesAsync(context, meshesService, "/paises", "BR");

                foreach (var polygon in polygons)
                {
                    var poligonoPaisEntity = new PoligonoPais()
                    {
                        Pais = entity,
                        Poligono = polygon
                    };

                    context.PoligonoPais.Add(poligonoPaisEntity);
                    await context.SaveChangesAsync();
                }
            }

            // Estado
            if (!context.Estado.Any())
            {
                logger.LogInformation("Estado Seed");

                var states = await placesService.ProcessPlaces<DivisionState>("/estados");                

                // Pais
                var country = context.Pais.Where(x => x.Nome == "Brasil").FirstOrDefault();

                var listEntity = new List<Estado>();

                var listPoligonoEstado = new List<PoligonoEstado>();

                foreach (var state in states)
                {
                    var entity = new Estado
                    {
                        Pais = country,
                        Nome = state.Name,
                        Sigla = state.Initials
                    };

                    listEntity.Add(entity);                    

                    var polygons = await SeedDefaultMeshesAsync(context, meshesService, "/estados", state.Id.ToString());

                    foreach (var polygon in polygons)
                    {                      
                        var poligonoEstadoEntity = new PoligonoEstado()
                        {
                            Estado = entity,
                            Poligono = polygon
                        };

                        listPoligonoEstado.Add(poligonoEstadoEntity);                        
                    }
                };

                context.PoligonoEstado.AddRange(listPoligonoEstado);
                await context.SaveChangesAsync();
            }

            // Cidade
            if (!context.Cidade.Any())
            {
                logger.LogInformation("Cidade Seed");

                var counties = await placesService.ProcessPlaces<DivisionCounty>("/municipios");

                var listEntity = new List<Cidade>();
                var listPoligonoCidadeEntity = new List<PoligonoCidade>();

                foreach (var county in counties)
                {
                    // Estado 
                    var state = context.Estado.Where(x => x.Nome == county.Microregiao.Mesoregion.State.Name).FirstOrDefault();

                    var entity = new Cidade
                    {
                        Estado = state,
                        Nome = county.Name
                    };

                    listEntity.Add(entity);

                    if (county.Name == "São Paulo")
                    {
                        var polygons = await SeedDefaultMeshesAsync(context, meshesService, "/municipios", county.Id.ToString());

                        foreach (var polygon in polygons)
                        {
                            var poligonoCidadeEntity = new PoligonoCidade()
                            {
                                Cidade = entity,
                                Poligono = polygon
                            };

                            listPoligonoCidadeEntity.Add(poligonoCidadeEntity);
                        }
                    }                    
                };

                context.Cidade.AddRange(listEntity);
                await context.SaveChangesAsync();

                context.PoligonoCidade.AddRange(listPoligonoCidadeEntity);
                await context.SaveChangesAsync();
            }

            // Distrito
            if (!context.Distrito.Any())
            {
                logger.LogInformation("Distrito Seed");

                var districts = await placesService.ProcessPlaces<DivisionDistrict>("/distritos");

                var listEntity = new List<Distrito>();

                foreach (var district in districts)
                {
                    // Cidade 
                    var county = context.Cidade.Where(x => x.Nome == district.County.Name).FirstOrDefault();

                    var entity = new Distrito
                    {
                        Cidade = county,
                        Nome = district.Name
                    };

                    listEntity.Add(entity);
                };

                context.Distrito.AddRange(listEntity);
                await context.SaveChangesAsync();
            }
        }

        private static async Task<List<Poligono>> SeedDefaultMeshesAsync(
            ApplicationDbContext context,
            IMeshesService meshesService,
            string path, 
            string identifier)
        {
            var geoJson = await meshesService.ProcessMeshes(path, "/" + identifier);

            var geoJsonType = geoJson.Geometry.Type;

            List<Feature<Polygon>> geoJsonListPolygons = null;

            var listPolygons = new List<Poligono>();

            switch (geoJsonType)
            {
                case GeoJSONObjectType.Polygon:
                    if (geoJsonListPolygons == null)
                    {
                        geoJsonListPolygons = new List<Feature<Polygon>>();

                        geoJsonListPolygons.Add(await meshesService.ProcessMeshes<Polygon>(path, "/" + identifier));
                    }                        

                    foreach (var geoJsonPolygon in geoJsonListPolygons)
                    {
                        // Polygon
                        var poligonoEntity = new Poligono();

                        listPolygons.Add(poligonoEntity);

                        // Point
                        var pontoListEntity = new List<Ponto>();
                        var poligonoPontoListEntity = new List<PoligonoPonto>();

                        foreach (var geoJsonLineString in geoJsonPolygon.Geometry.Coordinates)
                        {
                            foreach (var geoJsonPoint in geoJsonLineString.Coordinates)
                            {
                                var ponto = new Ponto()
                                {
                                    Latitude = geoJsonPoint.Latitude,
                                    Longitude = geoJsonPoint.Longitude
                                };

                                var poligonoPonto = new PoligonoPonto()
                                {
                                    Poligono = poligonoEntity,
                                    Ponto = ponto
                                };

                                pontoListEntity.Add(ponto);
                                poligonoPontoListEntity.Add(poligonoPonto);
                            }
                        }

                        context.PoligonoPonto.AddRange(poligonoPontoListEntity);
                        await context.SaveChangesAsync();
                    }     

                    break;
                case GeoJSONObjectType.MultiPolygon:
                    geoJsonListPolygons = new List<Feature<Polygon>>();

                    var geoJsonMultiPolygons = await meshesService.ProcessMeshes<MultiPolygon>(path, "/" + identifier);

                    foreach (var geoJsonPolygon in geoJsonMultiPolygons.Geometry.Coordinates)
                    {
                        var featurePolygon = new Feature<Polygon>(geoJsonPolygon);

                        geoJsonListPolygons.Add(featurePolygon);                            
                    }

                    goto case GeoJSONObjectType.Polygon;
                default:
                    throw new InvalidOperationException("unknown item type");
            }

            return listPolygons;
        }
    }
}
