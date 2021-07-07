using Application.Common.Enumerations;
using Application.Common.Interfaces;
using Infrastructure.AdministrativeDivision;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Places;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TupaDataBase"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }            

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(config =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(configuration["JWT:SecretKeyy"]);
                    var key = new SymmetricSecurityKey(secretBytes);

                    config.SaveToken = true;
                    config.RequireHttpsMetadata = false;

                    config.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.ContainsKey("access_token"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }

                            string authorization = context.Request.Headers["Authorization"];

                            if (string.IsNullOrEmpty(authorization))
                            {
                                context.NoResult();
                                return Task.CompletedTask;
                            }

                            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                            {
                                context.Token = authorization.Substring("Bearer ".Length).Trim();
                            }

                            if (string.IsNullOrEmpty(context.Token))
                            {
                                context.NoResult();
                                return Task.CompletedTask;
                            }

                            try
                            {
                                var part = context.Token.Split(".")[1];
                                var bytes = Convert.FromBase64String(part);
                                var stringsBytes = Encoding.UTF8.GetString(bytes).Replace("\"", "").Split(",");

                                foreach (var stringBytes in stringsBytes)
                                {
                                    if (stringBytes.Contains("uid"))
                                    {
                                        var uid = stringBytes.Split(":")[1];

                                        context.Request.QueryString = context.Request.QueryString.Add("UserId", uid);
                                    }
                                }
                            }
                            catch
                            {

                            }

                            return Task.CompletedTask;
                        }
                    };

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,

                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidAudience = configuration["JWT:ValidAudience"],
                        IssuerSigningKey = key,
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ElevatedRights", policy =>
                    policy.RequireRole(Roles.Manager.ToString(), Roles.Employee.ToString()));
            });

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPlacesService, PlacesService>();
            services.AddTransient<IMeshesService, MeshesService>();

            return services;
        }
    }
}
