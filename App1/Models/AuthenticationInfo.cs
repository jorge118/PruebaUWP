using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AuthenticationInfo
    {
        public string AccessToken { get; internal set; }

        public string AuthorizationScheme { get; internal set; }

        public string RefreshToken { get; internal set; }

        public string IdentityToken { get; internal set; }

        public DateTimeOffset AccessTokenExpiration { get; internal set; }

        public int AccessTokenExpiresIn { get; internal set; }
    }
}
