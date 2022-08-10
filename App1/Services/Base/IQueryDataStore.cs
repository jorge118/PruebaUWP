using App1.Models.Responses.Base;
using Reyma.Utils.Http.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQueryDataStore<TResponse> where TResponse : BaseModelResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<PagedResults<TResponse>> Get(long pageNumber, int pageSize, string filters = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TResponse> GetById(Guid id);
    }
}
