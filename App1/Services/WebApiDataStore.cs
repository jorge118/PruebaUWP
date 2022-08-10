using App1.Services.Infrastructure;
using Autofac;
using Reyma.Utils.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public abstract class WebApiDataStore : IDataStore
    {
        /// <summary>
        /// 
        /// </summary>
        protected IHttpClientService HttpClientUtil => App.Container.Resolve<IHttpClientService>();

        /// <summary>
        /// 
        /// </summary>
        protected abstract string ResourceUrl { get; }
    }
}
