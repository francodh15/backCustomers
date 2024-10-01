using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using BackMedicos.Models;
namespace BackMedicos
{
    public class jwtConfig
    {
        private readonly IConfiguration _configuration;

        public jwtConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string configJWT(string text)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                //COMPUTAR HASH
                byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                //Convertir array a bytes string

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public string generateJWT(Usuarios modelo)
        {
            // Crear User Claims
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,modelo.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name,modelo.UserName!),
                new Claim(ClaimTypes.Email,modelo.Correo!),
                new Claim(ClaimTypes.Role,modelo.Rol!)

            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //Detalle Token

            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
