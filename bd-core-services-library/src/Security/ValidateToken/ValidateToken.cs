using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography;

namespace BD.Core.Security.ValidateToken
{
    public class ValidateToken
    {
        #region Private variables
        private RsaConfig _rsa;
        private string knownConfig = "/.well-known/openid-configuration/jwks";
        private string endPoint;
        #endregion

        #region Constructors
        public ValidateToken(string url)
        {
            endPoint = url + knownConfig;
            if (_rsa == null) { GetSecurityRsaKeys(); }
        }
        #endregion

        #region Private Methods
        private void GetSecurityRsaKeys()
        {
            try
            {
                var webClient = new WebClient();
                var json = webClient.DownloadString(endPoint);
                var key = JsonConvert.DeserializeObject<dynamic>(json).keys[0];
                _rsa = new RsaConfig { Kid = key.kid, Exponent = key.e, Modulus = key.n };
            }
            catch (Exception ex)
            {
                throw new Exception($"Connection failed, detailed error is: " + ex.Message);
            }
        }
        #endregion

        #region Public Methods
        public bool TokenValidation(string token)
        {
            var keys = new List<SecurityKey>();
            var e = Base64Url.Decode(_rsa.Exponent);
            var n = Base64Url.Decode(_rsa.Modulus);
            var key = new RsaSecurityKey(new RSAParameters { Exponent = e, Modulus = n })
            {
                KeyId = _rsa.Kid
            };
            keys.Add(key);

            var parameters = new TokenValidationParameters
            {
                ValidIssuer = endPoint,
                ValidateAudience = false,
                //ValidAudience = audience,
                IssuerSigningKeys = keys,
                ValidateIssuer = false,
                NameClaimType = JwtClaimTypes.Name,
                RoleClaimType = JwtClaimTypes.Role,
                RequireExpirationTime = true,
                RequireSignedTokens = true
            };

            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();

            try
            {
                var x = handler.ValidateToken(token, parameters, out _);
                return x.Identity.IsAuthenticated;
            }
            catch (Exception ex)
            {
                throw new Exception($"Validation of the token failed, detailed error is: " + ex.Message);
            }
        }
        #endregion
    }
}
