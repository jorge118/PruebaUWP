using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Common
{
    public enum TipoConcepto
    {
        /// <summary>
        /// Alimentos
        /// </summary>
        [Description("Alimentos")]
        Alimentos = 0,

        /// <summary>
        /// Personas
        /// </summary>
        [Description("Personas")]
        Personas = 1,

        /// <summary>
        /// Mesas
        /// </summary>
        [Description("Mesas")]
        Mesas = 2
    }
}
