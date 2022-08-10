using App1.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Responses.Finanzas.Turnos
{
    public sealed class TurnoFormaPagoResponse : BaseModelResponse
    {
        /// <summary>
        /// Forma de Pago id.
        /// </summary>
        public Guid FormaPagoId { get; set; }

        /// <summary>
        /// Forma de pago.
        /// </summary>
        public string FormaPago { get; set; }

        /// <summary>
        /// Importe.
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EsBancaria { get; set; }
    }
}
