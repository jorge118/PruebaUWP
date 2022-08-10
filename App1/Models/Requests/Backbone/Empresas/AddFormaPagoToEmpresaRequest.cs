using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Requests.Backbone.Empresas
{
    public sealed class AddFormaPagoToEmpresaRequest : Bindable
    {
        /// <summary>
        /// Id de la empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id de la forma de pago
        /// </summary>
        public Guid FormaPagoId { get; set; }
    }
}
