using App1.Models.Common;
using App1.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Responses.Finanzas.Turnos
{
    public sealed class TurnoDetalleResponse : BaseModelResponse
    {
        /// <summary>
        /// Concepto Id.
        /// </summary>
        public Guid ConceptoId { get; set; }

        /// <summary>
        /// Concepto.
        /// </summary>
        public string Concepto { get; set; }

        /// <summary>
        /// Concepto Nombre Corto
        /// </summary>
        public string ConceptoNombreCorto { get; set; }

        /// <summary>
        /// Corte id.
        /// </summary>
        public Guid CorteId { get; set; }

        /// <summary>
        /// Corte.
        /// </summary>
        public string Corte { get; set; }

        /// <summary>
        /// Valor.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Es venta
        /// </summary>
        public bool EsVenta { get; set; }

        /// <summary>
        /// Permite negativos.
        /// </summary>
        public bool PermiteNegativos { get; set; }

        /// <summary>
        /// Orden del corte
        /// </summary>
        public decimal CorteOrden { get; set; }

        /// <summary>
        /// Orden del concepto
        /// </summary>
        public decimal ConceptoOrden { get; set; }

        /// <summary>
        /// Tipo
        /// </summary>
        public TipoConcepto ConceptoTipo { get; set; }
    }
}
