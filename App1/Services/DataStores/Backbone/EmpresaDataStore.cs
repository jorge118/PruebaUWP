using App1.Models.Common;

using App1.Models.Requests.Backbone.Empresas;
using App1.Models.Response.Backbone.Cortes;
using App1.Models.Responses.Backbone.Empresas;
using App1.Services.Base;
using Reyma.Utils.Http.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services.DataStores.Backbone
{
    public class EmpresaDataStore : WebApiDataStore, IQueryDataStore<EmpresaResponse>, IWriteDataStore<EmpresaRequest, EmpresaResponse>
    {
        protected override string ResourceUrl => "/api/backbone/empresas";

        public async Task<PagedResults<EmpresaResponse>> Get(long pageNumber, int pageSize, string filters = "")
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}";
            return await  HttpClientUtil.GetPagedResultsAsync<EmpresaResponse>(uri, new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize, Filters = filters }, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<IEnumerable<EmpresaResponse>> GetAll()
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/all";
            return await HttpClientUtil.GetCollectionAsync<EmpresaResponse>(uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<EmpresaResponse> GetById(Guid id)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{id}";
            return await HttpClientUtil.GetAsync<EmpresaResponse>(uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<EmpresaResponse> Create(EmpresaRequest request)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}";
            return await HttpClientUtil.PostAsync<EmpresaRequest, EmpresaResponse>(request, uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<EmpresaResponse> Update(EmpresaRequest request)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{request.Id}";
            return await HttpClientUtil.PutAsync<EmpresaRequest, EmpresaResponse>(request, uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task CancelarEmpresa(Guid id)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{id}";
            await HttpClientUtil.DeleteAsync(uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<EmpresaCorteResponse> AgregarCorte(AddCorteToEmpresaRequest request)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{request.EmpresaId}/cortes/{request.CorteId}";
            return await HttpClientUtil.PostAsync<AddCorteToEmpresaRequest, EmpresaCorteResponse>(request, uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task EliminarCorte(Guid empresaId, Guid corteId)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{empresaId}/cortes/{corteId}";
            await HttpClientUtil.DeleteAsync(uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<PagedResults<EmpresaCorteResponse>> GetCortes(Guid id)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{id}/cortes";
            return await HttpClientUtil.GetPagedResultsAsync<EmpresaCorteResponse>(uri, new PaginationSettings { PageNumber = 1, PageSize = 20, Filters = string.Empty }, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<IEnumerable<CorteConceptoResponse>> GetConceptos(Guid id)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{id}/conceptos";
            return await HttpClientUtil.GetCollectionAsync<CorteConceptoResponse>(uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task<EmpresaFormaPagoResponse> AgregarFormaPago(AddFormaPagoToEmpresaRequest request)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{request.EmpresaId}/formas_pago/{request.FormaPagoId}";
            return await HttpClientUtil.PostAsync<AddFormaPagoToEmpresaRequest, EmpresaFormaPagoResponse>(request, uri, AuthService.Instance.CreateSecuritySettings());
        }

        public async Task EliminarFormaPago(Guid empresaId, Guid formaPagoId)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{empresaId}/formas_pago/{formaPagoId}";
            await HttpClientUtil.DeleteAsync(uri, AuthService.Instance.CreateSecuritySettings());
        }


        public async Task<PagedResults<EmpresaFormaPagoResponse>> GetFormasPago(Guid empresaId)
        {
            var uri = $"{Constants.ApiUrlBase}{ResourceUrl}/{empresaId}/formas_pago";
            return await HttpClientUtil.GetPagedResultsAsync<EmpresaFormaPagoResponse>(uri, new PaginationSettings { PageNumber = 1, PageSize = 20, Filters = string.Empty }, AuthService.Instance.CreateSecuritySettings());
        }


    }
}
