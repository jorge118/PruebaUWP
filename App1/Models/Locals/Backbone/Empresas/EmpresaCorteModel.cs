using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Locals.Backbone.Empresas
{
    public sealed class EmpresaCorteModel : Bindable
    {

        public Guid Id { get; set; }

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
    }
}
