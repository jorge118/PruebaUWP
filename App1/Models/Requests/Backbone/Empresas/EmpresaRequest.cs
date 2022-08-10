using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Requests.Backbone.Empresas
{
    public sealed class EmpresaRequest
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Razón social.
        /// </summary>
        public string RazonSocial { get; set; }

        /// <summary>
        /// RFC.
        /// </summary>
        public string Rfc { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public byte[] Logo { get; set; }
    }
}
