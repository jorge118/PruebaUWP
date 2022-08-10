using System.Collections.Generic;

using IdentityModel.OidcClient.Browser;

namespace App1.Services
{
    public class AuthorityOptions
    {
        public string Authority { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUrl { get; set; }

        public IEnumerable<string> Scope { get; set; }

        public string AuthorizationScheme { get; set; } = AuthService.AuthorizationSchemeDefault;

        public IBrowser Browser { get; set; }

        public bool PersistRefreshToken { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string TokenEndPoint => $"{Authority}/connect/token";
    }
}
