using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

using Application.Common.Enumerations;
using Application.Common.Interfaces;
using Infrastructure.AdministrativeDivision;
using Infrastructure.Flooding;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Places;
using Infrastructure.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adiciona as configurações para se conectar com a fonte de dados e fontes externas.
        /// </summary>
        /// <remarks>
        /// Comandos úteis:
        /// <list type="bullet">
        /// <item>
        /// <term>Adicionar migrations</term>
        /// <description>dotnet ef migrations add "MigrationName" -s ../WebAPI/ -o ./Persistence/Migrations/</description>
        /// </item>
        /// <item>
        /// <term>Atualizar banco</term>
        /// <description>dotnet ef database update -s ../WebAPI/</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <returns>
        /// Retorna o <paramref name="services"/> com o serviços da infraestrutura.
        /// </returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TupaDataBase"));
            }
            else
            {
                if (configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
                    /*services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(
                            configuration.GetConnectionString("DevelopmentConnection"),
                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));*/
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySQL(
                            configuration.GetConnectionString("DevelopmentConnection")));
                else
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySQL(
                            configuration.GetConnectionString("ProductionConnection")));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
            })
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
                                var handler = new JwtSecurityTokenHandler();
                                var token = handler.ReadJwtToken(context.Token);

                                var keys = token.Payload.Keys;

                                foreach (var key in keys)
                                {
                                    if (key == "uid")
                                    {
                                        var uid = token.Payload[key].ToString();

                                        context.Request.QueryString = context.Request.QueryString.Add("UserId", uid);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new ArgumentOutOfRangeException(
                                    "ReadJwtToken", ex);
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

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
            services.AddTransient<IGoogleService, GoogleService>();
            services.AddTransient<IIdentityGetService, IdentityGetService>();
            services.AddTransient<ICGESPService, CGESPService>();
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
