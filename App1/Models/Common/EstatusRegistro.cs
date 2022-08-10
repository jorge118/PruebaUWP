using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Common
{
    public enum EstatusRegistro
    {
        [Description("Activo")]
        Activo,

        [Description("Cancelado")]
        Cancelado = -1
    }
}
