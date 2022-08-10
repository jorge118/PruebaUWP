using Reyma.Utils.Http.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services.Infrastructure
{
    public interface IHttpClientService
    {
        /// <summary>
        /// Realiza una llamada Post asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TRequest">Solicitud.</typeparam>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="request">Request que será enviada.</param>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string uri, HttpClientSettings httpClientSettings = null) where TRequest : class where TResponse : class;

        /// <summary>
        /// Realiza una llamada Post asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TRequest">Solicitud.</typeparam>
        /// <param name="request">Request que será enviada.</param>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{ByteArrayContent}"/></returns>
        Task<byte[]> PostAsync<TRequest>(TRequest request, string uri, HttpClientSettings httpClientSettings = null) where TRequest : class;

        /// <summary>
        /// Realiza una llamada Put asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TRequest">Solicitud.</typeparam>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="request">Request que será enviada.</param>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request, string uri, HttpClientSettings httpClientSettings = null) where TRequest : class where TResponse : class;

        /// <summary>
        /// Realiza una llamada Put asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TRequest">Solicitud.</typeparam>
        /// <param name="request">Request que será enviada.</param>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{ByteArrayContent}"/></returns>
        Task<byte[]> PutAsync<TRequest>(TRequest request, string uri, HttpClientSettings httpClientSettings = null) where TRequest : class;

        /// <summary>
        /// Realiza una llamada Delete asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResponse> DeleteAsync<TResponse>(string uri, HttpClientSettings httpClientSettings = null) where TResponse : class;

        /// <summary>
        /// Realiza una llamada Delete asincrona a una Web API.
        /// </summary>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{ByteArrayContent}"/></returns>
        Task<byte[]> DeleteAsync(string uri, HttpClientSettings httpClientSettings = null);

        /// <summary>
        /// Realiza una llamada Get asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResponse> GetAsync<TResponse>(string uri, HttpClientSettings httpClientSettings = null) where TResponse : class;

        /// <summary>
        /// Realiza una llamada Get asincrona a una Web API.
        /// </summary>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{ByteArrayContent}"/></returns>
        Task<byte[]> GetAsync(string uri, HttpClientSettings httpClientSettings = null);

        /// <summary>
        /// Realiza una llamada Get asincrona a una Web API y obtiene una colección de datos de la respuesta.
        /// </summary>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{IEnumerable}"/></returns>
        Task<IEnumerable<TResponse>> GetCollectionAsync<TResponse>(string uri, HttpClientSettings httpClientSettings = null) where TResponse : class;

        /// <summary>
        /// Realiza una llamada Post asincrona a una Web API y obtiene los resultados paginados de la respuesta.
        /// </summary>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="paginationSettings">Configuración de paginación.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{PagedResults}"/></returns>
        Task<PagedResults<TResponse>> GetPagedResultsAsync<TResponse>(string uri, PaginationSettings paginationSettings, HttpClientSettings httpClientSettings = null) where TResponse : class;
    }
}
