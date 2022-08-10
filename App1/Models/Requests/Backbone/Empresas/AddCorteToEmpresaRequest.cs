using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Requests.Backbone.Empresas
{
    public sealed class AddCorteToEmpresaRequest : Bindable
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CorteId { get; set; }
    }
}
