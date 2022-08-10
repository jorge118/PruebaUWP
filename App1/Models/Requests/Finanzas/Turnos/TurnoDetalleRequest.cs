using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Requests.Finanzas.Turnos
{
    public sealed class TurnoDetalleRequest : Bindable
    {
        /// <summary>
        /// Concepto Id.
        /// </summary>
        public Guid ConceptoId { get; set; }

        /// <summary>
        /// Corte id.
        /// </summary>
        public Guid CorteId { get; set; }

        /// <summary>
        /// Valor.
        /// </summary>
        public decimal Valor { get; set; }
    }
}
