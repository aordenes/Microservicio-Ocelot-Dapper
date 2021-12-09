using Cl.Dominio.Entidades.AccesosApi;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Transversal.Seguridad
{
    public class GenerarToken
    {
        public static string GenerateJwtToken(PersonaAcceso user, string secretKey)
        {
            //getting the secret key
           
            var key = Encoding.ASCII.GetBytes(secretKey);

            //Creacion de  claims
            //var claimAuthenticate = new Claim(ClaimTypes.Authentication, user.Passuser);
            var claimName = new Claim(ClaimTypes.Name, user.Nombre);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.PersonaId.ToString());

            //create claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] { claimName, claimNameIdentifier }, "serverAuth");

            // Genera un token que es valido por un 30 minutos.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(30),///DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //escribe y retorna el token
            return tokenHandler.WriteToken(token);
        }
    }
}
