using Application.Cidades.Queries.GetAllCidades;
using Application.Distritos.Queries.GetAllDistritos;
using Application.Estados.Queries.GetAllEstados;
using Application.Marcador.Commands.CreateMarcadores;
using Application.Marcador.Commands.DeleteMarcadores;
using Application.Marcador.Commands.UpdateMarcadores;
using Application.Marcador.Queries.GetAllMarcadores;
using Application.Paises.Queries.GetAllPaises;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region marcadores

            services
                .AddTransient<ICreateMarcadoresCommandHandler, CreateMarcadoresCommandHandler>()
                .AddTransient<IDeleteMarcadoresCommandHandler, DeleteMarcadoresCommandHandler>()
                .AddTransient<IUpdateMarcadoresCommandHandler, UpdateMarcadoresCommandHandler>()
                .AddTransient<IGetAllMarcadoresQueryHandler, GetAllMarcadoresQueryHandler>();

            #endregion

            #region paises

            services
                .AddTransient<IGetAllPaisesQueryHandler, GetAllPaisesQueryHandler>();

            #endregion

            #region estados

            services
                .AddTransient<IGetAllEstadosQueryHandler, GetAllEstadosQueryHandler>();

            #endregion

            #region cidade

            services
                .AddTransient<IGetAllCidadeQueryHandler, GetAllCidadeQueryHandler>();

            #endregion

            #region distrito

            services
                .AddTransient<IGetAllDistritosQueryHandler, GetAllDistritosQueryHandler>();

            #endregion

            return services;
        }
    }
}
