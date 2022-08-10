using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Requests.Finanzas.Turnos
{
    public sealed class TurnoFormaPagoRequest : Bindable
    {
        /// <summary>
        /// Forma de Pago id.
        /// </summary>
        public Guid FormaPagoId { get; set; }

        /// <summary>
        /// Importe.
        /// </summary>
        public decimal Importe { get; set; }
    }
}
