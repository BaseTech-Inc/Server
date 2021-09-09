using Application.Alertas.Commands.CreateAlertas;
using Application.Alertas.Queries.GetAlertasByDate;
using Application.Cidades.Queries.GetAllCidades;
using Application.Cidades.Queries.GetCidadesByName;
using Application.Distritos.Queries.GetAllDistritos;
using Application.Distritos.Queries.GetDistritosByName;
using Application.Estados.Queries.GetAllEstados;
using Application.Estados.Queries.GetEstadosByName;
using Application.Estados.Queries.GetPaisesByName;
using Application.Localidades.Queries.GetLocalidadesByNames;
using Application.Marcador.Commands.CreateMarcadores;
using Application.Marcador.Commands.DeleteMarcadores;
using Application.Marcador.Commands.UpdateMarcadores;
using Application.Marcador.Queries.GetAllMarcadores;
using Application.Paises.Queries.GetAllPaises;
using Application.Paises.Queries.GetPaisesByName;
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
                .AddTransient<IGetAllPaisesQueryHandler, GetAllPaisesQueryHandler>()
                .AddTransient<IGetPaisesByNameQueryHandler, GetPaisesByNameQueryHandler>();

            #endregion

            #region estados

            services
                .AddTransient<IGetAllEstadosQueryHandler, GetAllEstadosQueryHandler>()
                .AddTransient<IGetEstadosByNameQueryHandler, GetEstadosByNameQueryHandler>();

            #endregion

            #region cidade

            services
                .AddTransient<IGetAllCidadeQueryHandler, GetAllCidadeQueryHandler>()
                .AddTransient<IGetCidadesByNameQueryHandler, GetCidadesByNameQueryHandler>();

            #endregion

            #region distrito

            services
                .AddTransient<IGetAllDistritosQueryHandler, GetAllDistritosQueryHandler>()
                .AddTransient<IGetDistritosByNameQueryHandler, GetDistritosByNameQueryHandler>();

            #endregion

            #region Localidade

            services
                .AddTransient<IGetLocalidadeByNameQueryHandler, GetLocalidadeByNameQueryHandler>();

            #endregion

            #region Alertas

            services
                .AddTransient<ICreateAlertasCommandHandler, CreateAlertasCommandHandler>()
                .AddTransient<IGetAlertasByDateQueryHandler, GetAlertasByDateQueryHandler>();

            #endregion

            return services;
        }
    }
}
