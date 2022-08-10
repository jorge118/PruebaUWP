using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Requests.Finanzas.Turnos
{
    public sealed class TurnoRequest : Bindable
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Fecha.
        /// </summary>
        public DateTimeOffset Fecha { get; set; }

        /// <summary>
        /// Empresa id.
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Venta.
        /// </summary>
        public decimal Venta { get; set; }

        /// <summary>
        /// Saldo.
        /// </summary>
        public decimal Saldo { get; set; }

        /// <summary>
        /// Deposito.
        /// </summary>
        public decimal Deposito { get; set; }

        /// <summary>
        /// F7.
        /// </summary>
        public decimal F7 { get; set; }

        /// <summary>
        /// Importe Fiscal.
        /// </summary>
        public decimal ImporteFiscal { get; set; }

        /// <summary>
        /// Detalles.
        /// </summary>
        public IEnumerable<TurnoDetalleRequest> Detalles { get; set; }

        /// <summary>
        /// Formas de pago.
        /// </summary>
        public IEnumerable<TurnoFormaPagoRequest> FormasPago { get; set; }
    }
}
