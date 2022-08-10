using App1.Models.Base;
using App1.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Locals.Backbone.Empresas
{
    public sealed class EmpresaModel : Bindable
    {
        public Guid Id
        {
            get => GetProperty<Guid>();
            set => SetProperty(value);
        }

        public string RazonSocial
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string Rfc
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public EstatusRegistro Estatus
        {
            get => GetProperty<EstatusRegistro>();
            set => SetProperty(value);
        }

        public byte[] Logo
        {
            get => GetProperty<byte[]>();
            set => SetProperty(value);
        }
    }
}
