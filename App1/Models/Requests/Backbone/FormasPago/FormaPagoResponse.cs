using App1.Models.Common;
using App1.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Requests.Backbone.FormasPago
{
    public sealed class FormaPagoResponse : BaseModelResponse
    {
        /// <summary>
        /// Nombre.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Es bancarizada.
        /// </summary>
        public bool EsBancaria { get; set; }

        /// <summary>
        /// Estatus
        /// </summary>
        public EstatusRegistro Estatus
        {
            get => GetProperty<EstatusRegistro>();
            set => SetProperty(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
