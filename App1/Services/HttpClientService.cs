using App1.Services.Infrastructure;
using Autofac;
using Reyma.Utils.Http;
using Reyma.Utils.Http.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public class HttpClientService : IHttpClientService
    {

        private  IHttpResponseDeserializer _httpResponseDeserializer => App.Container.Resolve<IHttpResponseDeserializer>();
        private  IHttpContentBuilder _httpContentBuilder => App.Container.Resolve<IHttpContentBuilder>();
        private  IHttpResponseValidator _httpResponseValidator => App.Container.Resolve<IHttpResponseValidator>();
        private  IPagedGetUriBuilder _pagedGetUriBuilder => App.Container.Resolve<IPagedGetUriBuilder>();
        private  IHttpClientBuilder _httpClientBuilder => App.Container.Resolve<IHttpClientBuilder>();

        /// <summary>
        /// Realiza una llamada Post asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TRequest">Solicitud.</typeparam>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="request">Request que será enviada.</param>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string uri, HttpClientSettings httpClientSettings = null) where TRequest : class where TResponse : class
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                var content = await _httpContentBuilder.Build(request);

                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(PostAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Post", new { uri, request });

                var response = await ExecuteWithMetrics(async () => await client.PostAsync(uri, content), uri, request);

                await _httpResponseValidator.Validate(response, uri, request);

                return await _httpResponseDeserializer.GetResult<TResponse>(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Post asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TRequest">Solicitud.</typeparam>
        /// <param name="request">Request que será enviada.</param>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{ByteArrayContent}"/></returns>
        public async Task<byte[]> PostAsync<TRequest>(TRequest request, string uri, HttpClientSettings httpClientSettings = null) where TRequest : class
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                var content = await _httpContentBuilder.Build(request);

                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(PostAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Post", new { uri, request });

                var response = await ExecuteWithMetrics(async () => await client.PostAsync(uri, content), uri, request);

                await _httpResponseValidator.Validate(response, uri, request);

                return await _httpResponseDeserializer.GetResult(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Put asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TRequest">Solicitud.</typeparam>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="request">Request que será enviada.</param>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request, string uri, HttpClientSettings httpClientSettings = null) where TRequest : class where TResponse : class
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                var content = await _httpContentBuilder.Build(request);

                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(PutAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Put", new { uri, request });

                var response = await ExecuteWithMetrics(async () => await client.PutAsync(uri, content), uri, request);

                await _httpResponseValidator.Validate(response, uri, request);

                return await _httpResponseDeserializer.GetResult<TResponse>(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Put asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TRequest">Solicitud.</typeparam>
        /// <param name="request">Request que será enviada.</param>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{ByteArrayContent}"/></returns>
        public async Task<byte[]> PutAsync<TRequest>(TRequest request, string uri, HttpClientSettings httpClientSettings = null) where TRequest : class
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                var content = await _httpContentBuilder.Build(request);

                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(PutAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Put", new { uri, request });

                var response = await ExecuteWithMetrics(async () => await client.PutAsync(uri, content), uri, request);

                await _httpResponseValidator.Validate(response, uri, request);

                return await _httpResponseDeserializer.GetResult(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Delete asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public async Task<TResponse> DeleteAsync<TResponse>(string uri, HttpClientSettings httpClientSettings = null) where TResponse : class
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(DeleteAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Delete", new { uri });

                var response = await ExecuteWithMetrics(async () => await client.DeleteAsync(uri), uri);

                await _httpResponseValidator.Validate(response, uri);

                return await _httpResponseDeserializer.GetResult<TResponse>(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Delete asincrona a una Web API.
        /// </summary>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{ByteArrayContent}"/></returns>
        public async Task<byte[]> DeleteAsync(string uri, HttpClientSettings httpClientSettings = null)
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(DeleteAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Delete", new { uri });

                var response = await ExecuteWithMetrics(async () => await client.DeleteAsync(uri), uri);

                await _httpResponseValidator.Validate(response, uri);

                return await _httpResponseDeserializer.GetResult(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Get asincrona a una Web API.
        /// </summary>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public async Task<TResponse> GetAsync<TResponse>(string uri, HttpClientSettings httpClientSettings = null) where TResponse : class
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(GetAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Get", new { uri });

                var response = await ExecuteWithMetrics(async () => await client.GetAsync(uri), uri);

                await _httpResponseValidator.Validate(response, uri);

                return await _httpResponseDeserializer.GetResult<TResponse>(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Get asincrona a una Web API.
        /// </summary>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{ByteArrayContent}"/></returns>
        public async Task<byte[]> GetAsync(string uri, HttpClientSettings httpClientSettings = null)
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(GetAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Get", new { uri });

                var response = await ExecuteWithMetrics(async () => await client.GetAsync(uri), uri);

                await _httpResponseValidator.Validate(response, uri);

                return await _httpResponseDeserializer.GetResult(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Get asincrona a una Web API y obtiene una colección de datos de la respuesta.
        /// </summary>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{IEnumerable}"/></returns>
        public async Task<IEnumerable<TResponse>> GetCollectionAsync<TResponse>(string uri, HttpClientSettings httpClientSettings = null) where TResponse : class
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(GetCollectionAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Get", new { uri });

                var response = await ExecuteWithMetrics(async () => await client.GetAsync(uri), uri);

                await _httpResponseValidator.Validate(response, uri);

                return await _httpResponseDeserializer.GetResult<IEnumerable<TResponse>>(response);
            }
        }

        /// <summary>
        /// Realiza una llamada Post asincrona a una Web API y obtiene los resultados paginados de la respuesta.
        /// </summary>
        /// <typeparam name="TResponse">Respuesta.</typeparam>
        /// <param name="uri">Url que será invocada.</param>
        /// <param name="paginationSettings">Configuración de paginación.</param>
        /// <param name="httpClientSettings">Configuración de la llamada Http.</param>
        /// <returns><see cref="Task{PagedResults}"/></returns>
        public async Task<PagedResults<TResponse>> GetPagedResultsAsync<TResponse>(string uri, PaginationSettings paginationSettings, HttpClientSettings httpClientSettings = null) where TResponse : class
        {
            using (var client = await _httpClientBuilder.Build(httpClientSettings))
            {
                uri = await _pagedGetUriBuilder.Build(uri, paginationSettings);

                //if (_logger != null)
                //    _logger.LogInformation($"Track {nameof(GetPagedResultsAsync)} on {nameof(HttpClientService)}.", $"Calling to API. Resource Uri: {uri} Method: Get", new { uri, paginationSettings });

                var response = await ExecuteWithMetrics(async () => await client.GetAsync(uri), uri);

                await _httpResponseValidator.Validate(response, uri, paginationSettings);

                return await _httpResponseDeserializer.GetResult<PagedResults<TResponse>>(response);
            }
        }

        private async Task<HttpResponseMessage> ExecuteWithMetrics(Func<Task<HttpResponseMessage>> func, string uri, object request = default)
        {
            var stopWatch = new Stopwatch();

            if (HttpClientUtilService.EnableMetrics)
                stopWatch.Start();

            var response = await func();

            if (HttpClientUtilService.EnableMetrics)
            {
                stopWatch.Stop();

                //if (_logger != null)
                //    _logger.LogInformation("Track Metrics", $"Time Elapsed on API Call: {stopWatch.Elapsed}", new { uri, request });
            }

            return response;
        }
    }
}

