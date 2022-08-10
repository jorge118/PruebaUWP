using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Locals.Finanzas.Turnos
{
    public class TurnoDetalleModel : Bindable
    {
        public TurnoDetalleModel(Action onCalculateAction)
        {
             OnCalculateVenta = onCalculateAction;
        }

        /// <summary>
        /// Concepto Id.
        /// </summary>
        public Guid ConceptoId { get; set; }

        /// <summary>
        /// Concepto.
        /// </summary>
        public string Concepto
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Es venta
        /// </summary>
        public bool EsVenta
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Permite negativos.
        /// </summary>
        public bool PermiteNegativos
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Corte id.
        /// </summary>
        public Guid CorteId { get; set; }

        /// <summary>
        /// Corte.
        /// </summary>
        public string Corte
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Valor.
        /// </summary>
        public decimal Valor
        {
            get => GetProperty<decimal>();
            set 
            {
                SetProperty<decimal>(value);
                OnCalculateVenta();
            }
        }

        public Action OnCalculateVenta { get; set; }
    }
}
