using App1.Models.Base;
using App1.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Locals.Finanzas.Turnos
{
    public sealed class TurnoModel : Bindable
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Estatus
        /// </summary>
        public EstatusRegistro Estatus
        {
            get => GetProperty<EstatusRegistro>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Fecha.
        /// </summary>
        public DateTimeOffset Fecha
        {
            get => GetProperty<DateTimeOffset>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Empresa id.
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Empresa.
        /// </summary>
        public string Empresa
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }


        /// <summary>
        /// Venta.
        /// </summary>
        public decimal Venta
        {
            get => GetProperty<decimal>();
            set => SetProperty(value, () =>
            {
                F7 = Venta - ImporteFiscal;
            });
        }


        /// <summary>
        /// Saldo.
        /// </summary>
        public decimal Saldo
        {
            get => GetProperty<decimal>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Deposito.
        /// </summary>
        public decimal Deposito
        {
            get => GetProperty<decimal>();
            set => SetProperty(value);
        }

        /// <summary>
        /// F7.
        /// </summary>
        public decimal F7
        {
            get => GetProperty<decimal>();
            set => SetProperty(value, () =>
            {
                if (FormasPago != null)
                    Deposito = (Venta - FormasPago.Where(e => e.EsBancaria).Sum(e => e.Importe)) - F7;
            });
        }

        /// <summary>
        /// Importe Fiscal.
        /// </summary>
        public decimal ImporteFiscal
        {
            get => GetProperty<decimal>();
            set => SetProperty(value, () =>
            {
                F7 = Venta - ImporteFiscal;
            });
        }

        /// <summary>
        /// Detalles.
        /// </summary>
        public ObservableCollection<TurnoDetalleModel> Detalles
        {
            get => GetProperty<ObservableCollection<TurnoDetalleModel>>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Formas de pago.
        /// </summary>
        public ObservableCollection<TurnoFormaPagoModel> FormasPago
        {
            get => GetProperty<ObservableCollection<TurnoFormaPagoModel>>();
            set => SetProperty(value);
        }
    }
}
