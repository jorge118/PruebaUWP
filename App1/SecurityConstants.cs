using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class SecurityConstants
    {
        public static string Authority { get; private set; }

        public const string ClientId = "fnz.indicadores.winapp";

        public const string ClientSecret = "SW5zb21uaWEtNGQ1ZTM1NTctZjk4ZS00NTk0LThmMDgtOWI4ZTkwNjJmYjdiLTIwMjIwMjEw";

        public const string DataScheme = ClientId;

        public const string DataHost = "callback";

        public const string RedirectUri = DataScheme + "://" + DataHost;

        public static List<string> Scopes => new List<string>
        {
            "openid", "profile", "offline_access", "organizational", "permissions.service.read", "rym.appmobileinfo.read", "fnz.indicadores.write", "fnz.indicadores.read"
        };
    }
}
