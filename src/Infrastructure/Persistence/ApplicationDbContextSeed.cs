using Application.Common.Enumerations;
using Domain.Entities;
using Infrastructure.AdministrativeDivision;
using Infrastructure.GeoJson;
using Infrastructure.GeoJson.Geometry;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.AdministrativeDivision.DivisionLocation;

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

            var manager = new ApplicationUser { UserName = "manager@localhost", Email = "manager@localhost" };

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

        public static async Task SeedAdministrativeDivisionAsync(
            ApplicationDbContext context,
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

                var geoJson = await AdministrativeDivisionService.ProcessDivisionsMeshes("/paises", "/" + entity.Sigla);

                var geoJsonType = geoJson.Geometry.Type;

                if (geoJsonType == GeoJSONObjectType.Polygon)
                {
                    var geoJsonPolygon = await AdministrativeDivisionService.ProcessDivisionsMeshes<Polygon>("/paises", "/" + entity.Sigla);
                }
                else if (geoJsonType == GeoJSONObjectType.MultiPolygon)
                {
                    var geoJsonPolygons = await AdministrativeDivisionService.ProcessDivisionsMeshes<MultiPolygon>("/paises", "/" + entity.Sigla);

                    var listEntityPonto = new List<Ponto>();
                    var listEntityPoligono = new List<Poligono>();
                    var listEntityPoligonoPonto = new List<PoligonoPonto>();
                    var listEntityPoligonoPais = new List<PoligonoPais>();

                    foreach (var geoJsonMultiPolygon in geoJsonPolygons.Geometry.Coordinates)
                    {
                        foreach (var geoJsonPolygon in geoJsonMultiPolygon.Coordinates)
                        {
                            var poligono = new Poligono()
                            { };

                            listEntityPoligono.Add(poligono);

                            foreach (var geoJsonLineString in geoJsonPolygon.Coordinates)
                            {
                                foreach (var geoJsonPoint in geoJsonPolygon.Coordinates)
                                {
                                    var ponto = new Ponto()
                                    {
                                        Latitude = geoJsonPoint.Latitude,
                                        Longitude = geoJsonPoint.Longitude,
                                    };

                                    var poligonoPonto = new PoligonoPonto()
                                    {
                                        Poligono = poligono,
                                        Ponto = ponto
                                    };

                                    listEntityPonto.Add(ponto);
                                    listEntityPoligonoPonto.Add(poligonoPonto);                                    
                                }
                            }

                            var poligonoPais = new PoligonoPais()
                            {
                                Pais = entity,
                                Poligono = poligono
                            };

                            listEntityPoligonoPais.Add(poligonoPais);                            
                        }
                    }

                    context.Ponto.AddRange(listEntityPonto);
                    await context.SaveChangesAsync();

                    context.Poligono.AddRange(listEntityPoligono);
                    await context.SaveChangesAsync();

                    context.PoligonoPonto.AddRange(listEntityPoligonoPonto);
                    await context.SaveChangesAsync();

                    context.PoligonoPais.AddRange(listEntityPoligonoPais);
                    await context.SaveChangesAsync();
                }
            }

            // Estado
            if (!context.Estado.Any())
            {
                logger.LogInformation("Estado Seed");

                var states = await AdministrativeDivisionService.ProcessDivisionsLocations<DivisionState>("/estados");

                // Pais
                var country = context.Pais.Where(x => x.Nome == "Brasil").FirstOrDefault();

                var listEntity = new List<Estado>();

                foreach (var state in states)
                {
                    var entity = new Estado
                    {
                        Pais = country,
                        Nome = state.Name,
                        Sigla = state.Initials
                    };

                    listEntity.Add(entity);
                };

                context.Estado.AddRange(listEntity);

                await context.SaveChangesAsync();
            }

            // Cidade
            if (!context.Cidade.Any())
            {
                logger.LogInformation("Cidade Seed");

                var counties = await AdministrativeDivisionService.ProcessDivisionsLocations<DivisionCounty>("/municipios");

                var listEntity = new List<Cidade>();

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
                };

                context.Cidade.AddRange(listEntity);

                await context.SaveChangesAsync();
            }

            // Distrito
            if (!context.Distrito.Any())
            {
                logger.LogInformation("Distrito Seed");

                var districts = await AdministrativeDivisionService.ProcessDivisionsLocations<DivisionDistrict>("/distritos");

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

        private static async Task SeedAdministrativeDivisionMeshesAsync<T>()
        {

        }
    }
}
