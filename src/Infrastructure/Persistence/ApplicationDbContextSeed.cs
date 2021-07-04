﻿using Application.Common.Enumerations;
using Domain.Entities;
using Infrastructure.AdministrativeDivision;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static async Task SeedAdministrativeDivisionAsync(ApplicationDbContext context)
        {
            var administrativeDivisionService = new AdministrativeDivisionService();

            // Pais
            if (!context.Pais.Any())
            {
                var entity = new Pais
                {
                    Nome = "Brasil"
                };

                context.Pais.Add(entity);

                await context.SaveChangesAsync();
            }

            // Estado
            if (!context.Estado.Any())
            {
                var states = await administrativeDivisionService.ProcessDivisions<DivisionState>("/estados");

                // Pais
                var country = context.Pais.Where(x => x.Nome == "Brasil").FirstOrDefault();

                foreach (var state in states)
                {
                    var entity = new Estado
                    {
                        Pais = country,
                        Nome = state.Name,
                        Siglas = state.Initials
                    };

                    context.Estado.Add(entity);

                    await context.SaveChangesAsync();
                };
            }

            // Cidade
            if (!context.Cidade.Any())
            {               
                var counties = await administrativeDivisionService.ProcessDivisions<DivisionCounty>("/municipios");
                
                foreach (var county in counties)
                {
                    // Estado 
                    var state = context.Estado.Where(x => x.Nome == county.Microregiao.Mesoregion.State.Name).FirstOrDefault();

                    var entity = new Cidade
                    {
                        Estado = state,
                        Nome = county.Name
                    };

                    context.Cidade.Add(entity);

                    await context.SaveChangesAsync();
                };
            }

            // Distrito
            if (!context.Distrito.Any())
            {
                var districts = await administrativeDivisionService.ProcessDivisions<DivisionDistrict>("/distritos");

                foreach (var district in districts)
                {
                    // Cidade 
                    var county = context.Cidade.Where(x => x.Nome == district.County.Name).FirstOrDefault();

                    var entity = new Distrito
                    {
                        Cidade = county,
                        Nome = district.Name
                    };

                    context.Distrito.Add(entity);

                    await context.SaveChangesAsync();
                };
            }
        }
    }
}
