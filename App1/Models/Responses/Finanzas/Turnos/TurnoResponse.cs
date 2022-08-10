using App1.Models.Common;
using App1.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Responses.Finanzas.Turnos
{
    public sealed class TurnoResponse : BaseModelResponse
    {
        /// <summary>
        /// Fecha.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Empresa id.
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Empresa.
        /// </summary>
        public string Empresa { get; set; }

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
        /// Estatus.
        /// </summary>
        public EstatusRegistro Estatus
        {
            get => GetProperty<EstatusRegistro>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Detalles.
        /// </summary>
        public IEnumerable<TurnoDetalleResponse> Detalles { get; set; }

        /// <summary>
        /// Formas de pago.
        /// </summary>
        public IEnumerable<TurnoFormaPagoResponse> FormasPago { get; set; }
    }
}
