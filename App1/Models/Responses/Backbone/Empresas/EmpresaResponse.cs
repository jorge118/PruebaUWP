using App1.Models.Common;
using App1.Models.Responses.Base;


namespace App1.Models.Responses.Backbone.Empresas
{
    /// <summary>
    /// Empresa.
    /// </summary>
    public sealed class EmpresaResponse : BaseModelResponse
    {
        /// <summary>
        /// Razón social.
        /// </summary>
        public string RazonSocial { get; set; }

        /// <summary>
        /// RFC.
        /// </summary>
        public string Rfc { get; set; }

        /// <summary>
        /// Estatus
        /// </summary>
        public EstatusRegistro Estatus
        {
            get => GetProperty<EstatusRegistro>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Logo
        /// </summary>
        public byte[] Logo { get; set; }
    }
}
