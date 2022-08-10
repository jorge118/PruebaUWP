using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App1.AppConfiguration
{
    internal sealed class ApplicationVariable
    {
        internal MemberInfo MemberInfo { get; set; }

        internal object Value { get; set; }
    }
}
