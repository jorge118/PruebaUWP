using App1.Models.Common;
using App1.Models.Requests.Finanzas.Turnos;
using App1.Models.Responses.Finanzas.Turnos;
using App1.Services.Base;
using Reyma.Utils.Http.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services.DataStores.Finanzas
{
    public sealed class TurnoDataStore : WebApiDataStore, IWriteDataStore<TurnoRequest, TurnoResponse>, IQueryDataStore<TurnoResponse>
    {
        protected override string ResourceUrl => "/api/finanzas/turnos";


        public async Task<TurnoResponse> Create(TurnoRequest request)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}";
            return await HttpClientUtil.PostAsync<TurnoRequest, TurnoResponse>(request, uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<TurnoResponse> Update(TurnoRequest request)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{request.Id}";
            return await HttpClientUtil.PutAsync<TurnoRequest, TurnoResponse>(request, uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<PagedResults<TurnoResponse>> Get(long pageNumber, int pageSize, string filters = "")
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}";
            return await HttpClientUtil.GetPagedResultsAsync<TurnoResponse>(uri, new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize, Filters = filters }, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<TurnoResponse> GetById(Guid id)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{id}";
            return await HttpClientUtil.GetAsync<TurnoResponse>(uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task CancelarTurno(Guid id)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{id}";
            await HttpClientUtil.DeleteAsync(uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<IEnumerable<TurnoResponse>> GetTurnosByMes(Guid empresaId, Mes mes, int año)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/mes?EmpresaId={empresaId}&Mes={mes}&A%C3%B1o={año}";
            return await HttpClientUtil.GetCollectionAsync<TurnoResponse>(uri, AuthService.Instance.CreateSecuritySettings());
        }
    }
}
