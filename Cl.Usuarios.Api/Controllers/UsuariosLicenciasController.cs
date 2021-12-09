using Cl.Aplicacion.Contratos;
using Cl.Aplicacion.Dto.Request;
using Cl.Transversal.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cl.Usuarios.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuariosLicenciasController : ControllerBase
    {
        private readonly IPersonaLicenciaContrato<PersonaRequest> _personaLicenciaGenericRepository;
       
        private readonly ILoggerManager _LoggerManager;
        public UsuariosLicenciasController(IPersonaLicenciaContrato<PersonaRequest> personaLicenciaGenericRepository,                                            
                                           ILoggerManager LoggerManager)
        {
            _personaLicenciaGenericRepository = personaLicenciaGenericRepository;          
            _LoggerManager = LoggerManager;
        }



        [HttpGet("getlistusuarios")]
        public async Task<ActionResult<IEnumerable<PersonaRequest>>> GetListUsuarios()
        {
            try
            {
                _LoggerManager.LogDebug("Esto es una prueba");
                var personas = await _personaLicenciaGenericRepository.ObtenerUsuariosLicencia();
                return Ok(personas);
            }
            catch (Exception ex)
            {

                _LoggerManager.LogError(ex.Message + " " + ex.StackTrace);
                throw new Exception("esto es un error de prueba");
            }          
            
        }


        [HttpGet("getusuario/{idUser}")]
        public async Task<IActionResult> GetUsuario(int? idUser)
        {
            try
            {
                var persona = await _personaLicenciaGenericRepository.ObtenerUsuariosLicencia(idUser);
                return Ok(persona);
            }
            catch (Exception ex)
            {

                _LoggerManager.LogError(ex.Message + " " + ex.StackTrace);
                throw new Exception("esto es un error de prueba");
            }           

        }

        [HttpPost("agregausuario")]
        public async Task<IActionResult> AgregaUsuario(PersonaRequest personaRequest)
        {
            try
            {
                var retorno = await _personaLicenciaGenericRepository.InsertaPersonaNatural(personaRequest);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _LoggerManager.LogError(ex.Message + " " + ex.StackTrace);
                throw new Exception("esto es un error de prueba");
            }          
           
        }

        [HttpPost("agregalistausuarios")]
        public async Task<IActionResult> AgregaListaUsuarios(IEnumerable<PersonaRequest> lstpersonaRequest)
        {
            try
            {
                var retorno  = await _personaLicenciaGenericRepository.InsertaListaPersonas(lstpersonaRequest);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _LoggerManager.LogError(ex.Message + " " + ex.StackTrace);
                throw new Exception("esto es un error de prueba");
            }

        }

        [HttpDelete("deleteUsuario/{idUsuario}")]
        public async Task DeleteUsuario(int idUsuario)
        {
            try
            {
                await _personaLicenciaGenericRepository.EliminarPersona(idUsuario);
            }
            catch (Exception ex)
            {

                _LoggerManager.LogError(ex.Message + " " + ex.StackTrace);
                throw new Exception("esto es un error de prueba");
            }
        }

        [HttpPut("updateusuario")]
        public async Task UpdateUsuario(PersonaRequest personaRequest)
        {
            try
            {
                await _personaLicenciaGenericRepository.ActualizaUsuario(personaRequest);
            }
            catch (Exception ex)
            {

                _LoggerManager.LogError(ex.Message + " " + ex.StackTrace);
                throw new Exception("esto es un error de prueba");
            }
        
        }


    }
}
