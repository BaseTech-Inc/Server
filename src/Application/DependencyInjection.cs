using Application.Alertas.Commands.CreateAlertas;
using Application.Alertas.Commands.DeleteAlertas;
using Application.Alertas.Queries.GetAlertasByDate;
using Application.Cidades.Queries.GetAllCidades;
using Application.Cidades.Queries.GetCidadesById;
using Application.Cidades.Queries.GetCidadesByName;
using Application.Cidades.Queries.GetCidadesWithPagination;
using Application.Cidades.Queries.GetMeshesCidadeById;
using Application.Distritos.Queries.GetAllDistritos;
using Application.Distritos.Queries.GetDistritosById;
using Application.Distritos.Queries.GetDistritosByName;
using Application.Distritos.Queries.GetDistritosWithPagination;
using Application.Estados.Queries.GetAllEstados;
using Application.Estados.Queries.GetEstadosById;
using Application.Estados.Queries.GetEstadosByName;
using Application.Estados.Queries.GetEstadosWithPagination;
using Application.Estados.Queries.GetMeshesEstadosById;
using Application.Estados.Queries.GetPaisesByName;
using Application.HistoricoUsuarios.Commands.CreateHistorico;
using Application.HistoricoUsuarios.Commands.DeleteHistorico;
using Application.HistoricoUsuarios.Queries.GetAllHistorico;
using Application.HistoricoUsuarios.Queries.GetHistoricoById;
using Application.Localidades.Queries.GetLocalidadesByNames;
using Application.Marcador.Commands.CreateMarcadores;
using Application.Marcador.Commands.DeleteMarcadores;
using Application.Marcador.Commands.UpdateMarcadores;
using Application.Marcador.Queries.GetAllMarcadores;
using Application.Marcador.Queries.GetMarcadoresById;
using Application.Paises.Queries.GetAllPaises;
using Application.Paises.Queries.GetMeshesPaisesById;
using Application.Paises.Queries.GetPaisesById;
using Application.Paises.Queries.GetPaisesByName;
using Application.Paises.Queries.GetPaisesWithPagination;
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
                .AddTransient<IGetAllMarcadoresQueryHandler, GetAllMarcadoresQueryHandler>()
                .AddTransient<IGetMarcadoresByIdQueryHandler, GetMarcadoresByIdQueryHandler>();

            #endregion

            #region paises

            services
                .AddTransient<IGetAllPaisesQueryHandler, GetAllPaisesQueryHandler>()
                .AddTransient<IGetPaisesByIdQueryHandler, GetPaisesByIdQueryHandler>()
                .AddTransient<IGetPaisesByNameQueryHandler, GetPaisesByNameQueryHandler>()
                .AddTransient<IGetMeshesPaisesByIdQueryHandler, GetMeshesPaisesByIdQueryHandler>()
                .AddTransient<IGetPaisesWithPaginationQueryHandler, GetPaisesWithPaginationQueryHandler>();

            #endregion

            #region estados

            services
                .AddTransient<IGetAllEstadosQueryHandler, GetAllEstadosQueryHandler>()
                .AddTransient<IGetEstadosByIdQueryHandler, GetEstadosByIdQueryHandler>()
                .AddTransient<IGetMeshesEstadoByIdQueryHandler, GetMeshesEstadoByIdQueryHandler>()
                .AddTransient<IGetEstadosByNameQueryHandler, GetEstadosByNameQueryHandler>()
                .AddTransient<IGetEstadosWithPaginationQueryHandler, GetEstadosWithPaginationQueryHandler>();

            #endregion

            #region cidade

            services
                .AddTransient<IGetAllCidadeQueryHandler, GetAllCidadeQueryHandler>()
                .AddTransient<IGetCidadesByIdQueryHandler, GetCidadesByIdQueryHandler>()
                .AddTransient<IGetCidadesByNameQueryHandler, GetCidadesByNameQueryHandler>()
                .AddTransient<IGetMeshesCidadesByIdQueryHandler, GetMeshesCidadesByIdQueryHandler>()
                .AddTransient<IGetCidadesWithPaginationQueryHandler, GetCidadesWithPaginationQueryHandler>();

            #endregion

            #region distrito

            services
                .AddTransient<IGetAllDistritosQueryHandler, GetAllDistritosQueryHandler>()
                .AddTransient<IGetDistritosByIdQueryHandler, GetDistritosByIdQueryHandler>()
                .AddTransient<IGetDistritosByNameQueryHandler, GetDistritosByNameQueryHandler>()
                .AddTransient<IGetDistritosWithPaginationQueryHandler, GetDistritosWithPaginationQueryHandler>();

            #endregion

            #region Localidade

            services
                .AddTransient<IGetLocalidadeByNameQueryHandler, GetLocalidadeByNameQueryHandler>();

            #endregion

            #region Alertas

            services
                .AddTransient<ICreateAlertasCommandHandler, CreateAlertasCommandHandler>()
                .AddTransient<IDeleteAlertasCommandHandler, DeleteAlertasCommandHandler>()
                .AddTransient<IGetAlertasByDateQueryHandler, GetAlertasByDateQueryHandler>();

            #endregion

            #region HistoricoUsuario

            services
                .AddTransient<ICreateHistoricoCommandHandler, CreateHistoricoCommandHandler>()
                .AddTransient<IDeleteHistoricoCommandHandler, DeleteHistoricoCommandHandler>()
                .AddTransient<IGetAllHistoricoQueryHandler, GetAllHistoricoQueryHandler>()
                .AddTransient<IGetHistoricoByIdQueryHandler, GetHistoricoByIdQueryHandler>();

            #endregion

            return services;
        }
    }
}
