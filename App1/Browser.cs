using App1.Extensions;
using App1.Models;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Authentication.Web;

namespace App1
{
    public class Browser : IBrowser
    {
        private readonly bool _enableWindowsAuthentication;
        public Browser(bool enableWindowsAuthentication = false)
        {
            _enableWindowsAuthentication = enableWindowsAuthentication;
        }

        private async Task<BrowserResult> InvokeAsyncCore(BrowserOptions options, bool silentMode)
        {
            var wabOptions = WebAuthenticationOptions.None;

            WebAuthenticationResult wabResult;

            try
            {


                wabResult = await WebAuthenticationBroker.AuthenticateAsync(
                        wabOptions, new Uri(options.StartUrl), new Uri(SecurityConstants.RedirectUri));
            }
            catch (Exception ex)
            {
                return new BrowserResult
                {
                    ResultType = BrowserResultType.UnknownError,
                    Error = ex.ToString()
                };
            }

            if (wabResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                return new BrowserResult
                {
                    ResultType = BrowserResultType.Success,
                    Response = ParseAuthenticatorResult(wabResult.ResponseData)
                };
            }
            else
            {
                return new BrowserResult
                {
                    ResultType = BrowserResultType.UnknownError,
                    Error = "Invalid response from WebAuthenticationBroker"
                };
            }
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {

            if (string.IsNullOrWhiteSpace(options.StartUrl)) throw new ArgumentException("Missing StartUrl", nameof(options));
            if (string.IsNullOrWhiteSpace(options.EndUrl)) throw new ArgumentException("Missing EndUrl", nameof(options));

            switch (options.DisplayMode)
            {
                case DisplayMode.Visible:
                    return await InvokeAsyncCore(options, false);

                case DisplayMode.Hidden:
                    var result = await InvokeAsyncCore(options, true);
                    if (result.ResultType == BrowserResultType.Success)
                    {
                        return result;
                    }
                    else
                    {
                        result.ResultType = BrowserResultType.Timeout;
                        return result;
                    }
            }

            throw new ArgumentException("Invalid DisplayMode", nameof(options));
        }


        string ParseAuthenticatorResult(string responseData)
        {

            var decoder = new WwwFormUrlDecoder(responseData);

            var error = decoder?.TryGetValue("error");
            if (error != null)
                return string.Empty;

            string code = decoder?.TryGetValue("fnz.indicadores.winapp://callback/?code");
            string scope = decoder?.TryGetValue("scope");
            string state = decoder?.TryGetValue("state");
            string sessionState = decoder?.TryGetValue("session_state");
            var url = $"{SecurityConstants.RedirectUri}#code={code}&scope={scope}&state={state}&session_state={sessionState}";

            return url;
        }

    }

}

