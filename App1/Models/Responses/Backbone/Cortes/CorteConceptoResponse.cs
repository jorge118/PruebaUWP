using App1.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Response.Backbone.Cortes
{
    public sealed class CorteConceptoResponse : BaseModelResponse
    {
        /// <summary>
        /// Corte id.
        /// </summary>
        public Guid CorteId { get; set; }

        /// <summary>
        /// Corte.
        /// </summary>
        public string Corte { get; set; }

        /// <summary>
        /// Concepto id.
        /// </summary>
        public Guid ConceptoId { get; set; }

        /// <summary>
        /// Concepto.
        /// </summary>
        public string Concepto { get; set; }


        /// <summary>
        /// Es venta
        /// </summary>
        public bool EsVenta { get; set; }

        /// <summary>
        /// Permite negativos.
        /// </summary>
        public bool PermiteNegativos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal CorteOrden { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ConceptoOrden { get; set; }
    }
}
