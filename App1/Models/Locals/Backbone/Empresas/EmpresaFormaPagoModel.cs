using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Locals.Backbone.Empresas
{
    public class EmpresaFormaPagoModel : Bindable
    {

        public Guid Id { get; set; }

        /// <summary>
        /// Id de la empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Empresa
        /// </summary>
        public string Empresa
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Id de la forma de pago
        /// </summary>
        public Guid FormaPagoId { get; set; }

        /// <summary>
        /// Forma de Pago
        /// </summary>
        public string FormaPago
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
    }
}
