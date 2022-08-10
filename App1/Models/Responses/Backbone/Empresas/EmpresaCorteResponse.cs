using App1.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Responses.Backbone.Empresas
{
    public sealed class EmpresaCorteResponse : BaseModelResponse
    {
        /// <summary>
        /// Empresa id.
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Empresa.
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Corte id.
        /// </summary>
        public Guid CorteId { get; set; }

        /// <summary>
        /// Corte.
        /// </summary>
        public string Corte { get; set; }
    }
}
