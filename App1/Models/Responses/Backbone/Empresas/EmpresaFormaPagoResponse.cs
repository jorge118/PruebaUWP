using App1.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Responses.Backbone.Empresas
{
    public sealed class EmpresaFormaPagoResponse : BaseModelResponse
    {
        /// <summary>
        /// Id de la empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Empresa
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Id de la forma de pago
        /// </summary>
        public Guid FormaPagoId { get; set; }

        /// <summary>
        /// Forma de Pago
        /// </summary>
        public string FormaPago { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EsBancaria { get; set; }
    }
}
