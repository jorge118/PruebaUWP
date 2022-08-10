using App1.Exceptions;
using App1.Models;
using Autofac;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Reyma.Utils.Http;
using Reyma.Utils.Http.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

using Reyma.IdentityService.Extensions;

using UserType = App1.Models.UserType;
using App1.Services.Infrastructure;

namespace App1.Services
{
    public class AuthService
    {
        internal const string AuthorizationSchemeDefault = "Bearer";

        internal const string RefreshTokenGrantType = "refresh_token";

        private static AuthService _instance;

        public static AuthService Instance
        {
            get
            {
                if (_instance == null)
                    throw new AuthServiceInitializationException("AuthService isn't initialized. Should call 'Init' method and setup the service.");

                return _instance;
            }
        }

        internal AuthorityOptions AuthorityOptions { get; private set; }

        public AuthenticationInfo AuthenticationInfo { get; private set; }

        public UserProfile UserProfile { get; private set; }

        internal DiscoveryDocumentResponse DiscoveryDocument { get; private set; }

        public bool IsAuthenticated { get; set; }

        private AuthService() { }

        public static void Init(AuthorityOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            _instance = new AuthService
            {
                AuthorityOptions = options
            };

            Task.Run(async () =>
            {
                _instance.DiscoveryDocument = await new HttpClient().GetDiscoveryDocumentAsync(_instance.AuthorityOptions.Authority);
            });

        }

        public async Task LoginAsync()
        {
            var loginResult = await new OidcClient(GetOidcClientOptions()).LoginAsync(new LoginRequest());

            AuthenticationInfo = GetAuthenticationInfo(loginResult);
            UserProfile = await GetUserProfile(loginResult.User);
            IsAuthenticated = true;
        }

        public async Task RefreshToken()
        {
            if (!IsAuthenticated)
                throw new InvalidOperationException("There not an user account authenticated.");

            var response = await new HttpClient().RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = DiscoveryDocument.TokenEndpoint,
                ClientId = AuthorityOptions.ClientId,
                GrantType = RefreshTokenGrantType,
                RefreshToken = AuthenticationInfo.RefreshToken
            });

            AuthenticationInfo = GetAuthenticationInfo(response);
        }

        public async Task LogoutAsync()
        {
            await new OidcClient(GetOidcClientOptions()).LogoutAsync(new LogoutRequest { IdTokenHint = AuthenticationInfo?.IdentityToken });

            AuthenticationInfo = default;
            UserProfile = default;
            IsAuthenticated = false;
        }

        public HttpClientSettings CreateSecuritySettings() => new HttpClientSettings { AuthorizationScheme = AuthenticationInfo.AuthorizationScheme, AuthorizationToken = AuthenticationInfo.AccessToken };

        private OidcClientOptions GetOidcClientOptions() => new OidcClientOptions
        {
            Authority = AuthorityOptions.Authority,
            ClientId = AuthorityOptions.ClientId,
            Scope = string.Join(" ", AuthorityOptions.Scope),
            RedirectUri = AuthorityOptions.RedirectUrl,
            Browser = AuthorityOptions.Browser,
            IdentityTokenValidator = new TokenValidator()
        };

        private AuthenticationInfo GetAuthenticationInfo(LoginResult loginResult) => new AuthenticationInfo
        {
            AccessToken = loginResult.AccessToken,
            AuthorizationScheme = AuthorityOptions.AuthorizationScheme,
            IdentityToken = loginResult.IdentityToken,
            RefreshToken = loginResult.RefreshToken,
            AccessTokenExpiration = loginResult.AccessTokenExpiration,
            AccessTokenExpiresIn = (int)loginResult.AccessTokenExpiration.Subtract(DateTime.Now).TotalSeconds
        };

        private AuthenticationInfo GetAuthenticationInfo(TokenResponse tokenResponse) => new AuthenticationInfo
        {
            AccessToken = tokenResponse.AccessToken,
            AuthorizationScheme = AuthorityOptions.AuthorizationScheme,
            IdentityToken = tokenResponse.IdentityToken,
            RefreshToken = tokenResponse.RefreshToken,
            AccessTokenExpiration = DateTimeOffset.Now.AddSeconds(tokenResponse.ExpiresIn),
            AccessTokenExpiresIn = tokenResponse.ExpiresIn
        };

        private async Task<UserProfile> GetUserProfile(ClaimsPrincipal principal)
        {
            //var userInfo = await new HttpClient().GetUserInfoAsync(new UserInfoRequest { Token = AuthenticationInfo.AccessToken });
            var userId = principal.GetUserId();

            //var dialog = App.Container.Resolve<IDialogService>();

            //var httpclient = App.Container.Resolve<IHttpClientUtil>();

            var userProfile = await App.Container.Resolve<IHttpClientService>().GetAsync<UserProfile>($"{AuthorityOptions.Authority}/api/accounts/{principal.GetUserId()}/public_profile");

            //var userProfile = new UserProfile();

            userProfile.NickName = principal.GetUserName();
            userProfile.Id = principal.GetUserId();
            userProfile.Roles = principal.GetRoles();
            userProfile.EmployeeId = principal.GetIdentityEmployeeId();
            userProfile.Branches = principal.GetBranchs();
            userProfile.ClientId = principal.GetClientId();
            userProfile.UserType = userProfile.EmployeeId != Guid.Empty ? UserType.Employee : UserType.Client;

            return userProfile;
        }

    }
}
