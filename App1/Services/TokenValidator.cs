using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App1.Services
{
    public sealed class TokenValidator : IIdentityTokenValidator
    {
        private const string Algorithm = "PS256";
        private const string KeyType = "RSA";

        public Task<IdentityTokenValidationResult> ValidateAsync(string identityToken, OidcClientOptions options, CancellationToken cancellationToken = default)
            => Task.FromResult(new IdentityTokenValidationResult
            {
                SignatureAlgorithm = Algorithm,
                User = ValidateToken(identityToken)
            });

        public ClaimsPrincipal ValidateToken(string identityToken)
        {
            var result = new JsonWebTokenHandler().ValidateToken(identityToken,
                new TokenValidationParameters
                {
                    ValidIssuer = AuthService.Instance.AuthorityOptions.Authority,
                    ValidAudience = AuthService.Instance.AuthorityOptions.ClientId,
                    IssuerSigningKeys = GetSecurityKeys()
                });

            if (!result.IsValid) throw new InvalidOperationException("Token is not valid.");

            return new ClaimsPrincipal(result.ClaimsIdentity);
        }

        private List<SecurityKey> GetSecurityKeys()
        {
            var keys = new List<SecurityKey>();

            foreach (var key in AuthService.Instance.DiscoveryDocument.KeySet.Keys)
            {
                if (key.Kty != KeyType)
                    throw new NotImplementedException("Only RSA key type is implemented for token validation");

                if (key.X5c != null && key.X5c.Count > 0)
                {
                    keys.Add(GetX509SecurityKey(key));
                }
                else if (!string.IsNullOrWhiteSpace(key.E) && !string.IsNullOrWhiteSpace(key.N))
                {
                    keys.Add(GetRsaSecurityKey(key));
                }
                else
                {
                    throw new InvalidOperationException("JWK data is missing in token validation");
                }
            }

            return keys;
        }

        private X509SecurityKey GetX509SecurityKey(IdentityModel.Jwk.JsonWebKey key)
        {
            string certificateString = key.X5c.First();
            var certificate = new X509Certificate2(Convert.FromBase64String(certificateString));

            return new X509SecurityKey(certificate)
            {
                KeyId = key.Kid
            };
        }

        private RsaSecurityKey GetRsaSecurityKey(IdentityModel.Jwk.JsonWebKey key)
        {
            byte[] exponent = Base64UrlDecode(key.E);
            byte[] modulus = Base64UrlDecode(key.N);

            var rsaParameters = new RSAParameters
            {
                Exponent = exponent,
                Modulus = modulus
            };

            return new RsaSecurityKey(rsaParameters)
            {
                KeyId = key.Kid
            };
        }

        private byte[] Base64UrlDecode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default:
                    throw new Exception("Illegal base64url string!");
            }

            return Convert.FromBase64String(s); // Standard base64 decoder
        }
    }
}
