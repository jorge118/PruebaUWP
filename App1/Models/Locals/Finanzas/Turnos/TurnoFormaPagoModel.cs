using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Locals.Finanzas.Turnos
{
    public class TurnoFormaPagoModel : Bindable
    {
        public TurnoFormaPagoModel(Action onCalculateAction)
        {
            OnCalculateImporte = onCalculateAction;
        }

        public TurnoFormaPagoModel()
        {

        }

        /// <summary>
        /// Forma de Pago id.
        /// </summary>
        public Guid FormaPagoId { get; set; }

        /// <summary>
        /// Forma de pago.
        /// </summary>
        public string FormaPago
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Importe.
        /// </summary>
        public decimal Importe
        {
            get => GetProperty<decimal>();
            set
            {
                SetProperty(value);
                OnCalculateImporte();
            }
        }

        /// <summary>
        /// Es Bancaria.
        /// </summary>
        public bool EsBancaria
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public Action OnCalculateImporte { get; }
    }
}
