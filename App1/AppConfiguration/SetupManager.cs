using App1.Services;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.AppConfiguration
{
    internal static class SetupManager
    {
        internal static void Apply()
        {
            AuthService.Init(new AuthorityOptions
            {
                Authority = SecurityConstants.Authority,
                ClientId = SecurityConstants.ClientId,
                ClientSecret = SecurityConstants.ClientSecret,
                RedirectUrl = SecurityConstants.RedirectUri,
                Scope = SecurityConstants.Scopes,
                Browser = new Browser(),
                PersistRefreshToken = true,
                
            });
        }
    }
}
