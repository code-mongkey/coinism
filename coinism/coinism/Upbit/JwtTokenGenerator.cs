using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace coinism.Upbit
{
    public static class JwtTokenGenerator
    {
        public static string Generate(string accessKey, string secretKey, Dictionary<string, string> query = null)
        {
            var payload = new JwtPayload
            {
                { "access_key", accessKey },
                { "nonce", Guid.NewGuid().ToString() }
            };

            if (query != null && query.Count > 0)
            {
                var queryString = string.Join("&", query.Select(kv => $"{kv.Key}={kv.Value}"));
                using var sha512 = SHA512.Create();
                var queryHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(queryString));
                var queryHashHex = BitConverter.ToString(queryHash).Replace("-", "").ToLower();

                payload.Add("query_hash", queryHashHex);
                payload.Add("query_hash_alg", "SHA512");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
