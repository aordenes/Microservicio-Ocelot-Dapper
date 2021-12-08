using Cl.Licencias.Medicas.Aplicacion.Contratos;
using Cl.Licencias.Medicas.Aplicacion.Dto.Request;
using Cl.Licencias.Medicas.Aplicacion.Dto.Response;
using Cl.Licencias.Medicas.Dominio.Entidades.AccesosApi;
using Cl.Licencias.Medicas.Transversal.Log;
using Cl.Licencias.Medicas.Transversal.Seguridad;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Accesos.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticarController : ControllerBase
    {
        private readonly IConfiguration _configuration;       
        private readonly IAccesoContrato<PersonaAcceso> _personaGenericRepository;
        private readonly ILoggerManager _LoggerManager;

        public AutenticarController(IConfiguration configuration, IAccesoContrato<PersonaAcceso> personaGenericRepository, ILoggerManager LoggerManager)
        {
            _configuration = configuration;
            _personaGenericRepository = personaGenericRepository;
            _LoggerManager = LoggerManager;
        }

        [HttpPost("authenticatejwt")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateJWT(AuthenticationRequest authenticationRequest)
        {
            string token = string.Empty;

          
            try
            {   //checking if the user exists in the database          
                var personasExiste = await _personaGenericRepository.GetPersonaByNombreAndPass(authenticationRequest);
                if (personasExiste != null)
                {
                    //generating the token
                    token = GenerarToken.GenerateJwtToken(personasExiste, _configuration["JWTSettings:SecretKey"]);
                }
            }
            catch (Exception ex)
            {

                _LoggerManager.LogError(ex.Message + " " + ex.StackTrace);
                
                return await Task.FromResult(new AuthenticationResponse() { Token = token });
                
            }
            
            return await Task.FromResult(new AuthenticationResponse() { Token = token });
        }

        [HttpPost("getuserbyjwt")]
        public async Task<ActionResult<PersonaAcceso>> GetUserByJWT([FromBody] string jwtToken)
        {
            try
            {
                //getting the secret key
                string secretKey = _configuration["JWTSettings:SecretKey"];
                var key = Encoding.ASCII.GetBytes(secretKey);

                //preparing the validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                //Valida el token
                var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    //returning the user if found
                    var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    //return await _context.Users.Where(u => u.UserId == Convert.ToInt64(userId)).FirstOrDefaultAsync();
                    //var persona = await _personaGenericRepository.GetPersonaByEmail(userId);
                    return await _personaGenericRepository.GetPersonaId(Convert.ToUInt16(userId));
                }
            }
            catch (Exception ex)
            {
                //logging the error and returning null
                _LoggerManager.LogError(ex.Message + " " + ex.StackTrace);
               
                ///return await Task.FromResult(new AuthenticationResponse() { Token = token });
                //return null;
            }
            //returning null if token is not validated
            return null;
        }
    }
}
