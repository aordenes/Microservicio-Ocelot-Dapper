using Cl.Aplicacion.Contratos;
using Cl.Aplicacion.Dto.Request;
using Cl.Dal.Helper;
using Cl.Dal.IRepositorioOracle;
using Cl.Dominio.Recursos;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Dominio.Servicios
{
    public class PersonaLicenciaContrato<T> : IPersonaLicenciaContrato<T> where T : class
    {
        private readonly IOracleRepository<T> _oracleRepository;

        public PersonaLicenciaContrato(IOracleRepository<T> oracleRepository)
        {
            _oracleRepository = oracleRepository;
        }

        public async Task<IEnumerable<T>> ObtenerUsuariosLicencia()
        {
            IEnumerable<T> personas;
            try
            {
                var dynamicParameter = new OracleDynamicParameters();
                dynamicParameter.Add("PERSONACURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                //personas = await _oracleRepository.GetAll(dynamicParameter, SpRecursoUsuarioLicencias.USP_GETPERSONAS);
                personas = await _oracleRepository.GetAll(dynamicParameter, SpRecursoUsuarioLicencias.PRUEBAPAQUETE_USP_GETPERSONAS);


            }
            catch 
            {
                throw;
            }


            return personas;
        }

        public async Task<T> ObtenerUsuariosLicencia(int? id)
        {

            T persona;
            try
            {
                var dynamicParameter = new OracleDynamicParameters();
                dynamicParameter.Add("PERSONACURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                dynamicParameter.Add("ID", OracleDbType.Int32, ParameterDirection.Input, id);

                persona = await _oracleRepository.Get(dynamicParameter, SpRecursosAccesos.USP_GETPERSONA);


            }
            catch
            {
                throw;
            }


            return persona;
        }

        public async Task<bool> InsertaPersonaNatural(PersonaRequest personaRequest)
        {
            
            try
            {
                var dynamicParameter = new OracleDynamicParameters();
                dynamicParameter.Add("PER_NOMBRE", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Nombre);
                dynamicParameter.Add("PER_APELLIDO", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Apellido);
                dynamicParameter.Add("PER_DIRECCION", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Direccion);
                dynamicParameter.Add("PER_TELEFONO", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Telefono);
                dynamicParameter.Add("PER_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Email);
                dynamicParameter.Add("PER_PASSUSER", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Passuser);

                return  await _oracleRepository.Insert(dynamicParameter, SpRecursoUsuarioLicencias.USP_INSERTA_PERSONA);
              
            }
            catch 
            {
                throw;
            }
            
        }

        public async Task<bool> InsertaListaPersonas(IEnumerable<PersonaRequest> lstPersonaRequest)
        {
            
            try
            {
                List<OracleDynamicParameters> lstoracleDynamicParameters = new();

                foreach (var itemPersona in lstPersonaRequest)
                {
                    var dynamicParameter = new OracleDynamicParameters();

                    dynamicParameter.Add("PER_NOMBRE", OracleDbType.Varchar2, ParameterDirection.Input, itemPersona.Nombre);
                    dynamicParameter.Add("PER_APELLIDO", OracleDbType.Varchar2, ParameterDirection.Input, itemPersona.Apellido);
                    dynamicParameter.Add("PER_DIRECCION", OracleDbType.Varchar2, ParameterDirection.Input, itemPersona.Direccion);
                    dynamicParameter.Add("PER_TELEFONO", OracleDbType.Varchar2, ParameterDirection.Input, itemPersona.Telefono);
                    dynamicParameter.Add("PER_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input, itemPersona.Email);
                    dynamicParameter.Add("PER_PASSUSER", OracleDbType.Varchar2, ParameterDirection.Input, itemPersona.Passuser);


                    lstoracleDynamicParameters.Add(dynamicParameter);
                }

               return await _oracleRepository.InsertMany(lstoracleDynamicParameters, SpRecursoUsuarioLicencias.USP_INSERTA_PERSONA);                

            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Elimina un usuario por medio de un Id
        /// </summary>
        /// <param name="idPersona">id persona</param>
        /// <returns></returns>
        public async Task EliminarPersona(int idPersona)
        {
            
            try
            {
                var dynamicParameter = new OracleDynamicParameters();
                dynamicParameter.Add("PER_ID", OracleDbType.Int32, ParameterDirection.Input, idPersona);
                

                await _oracleRepository.Delete(dynamicParameter, SpRecursoUsuarioLicencias.USP_ELIMINA_PERSONA);
            
            }
            catch 
            {
                throw;
            }
           
        }

        public async Task ActualizaUsuario(PersonaRequest personaRequest)
        {

            try
            {
                var dynamicParameter = new OracleDynamicParameters();
                dynamicParameter.Add("ID", OracleDbType.Int32, ParameterDirection.Input, personaRequest.PersonaId);
                dynamicParameter.Add("PER_NOMBRE", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Nombre);
                dynamicParameter.Add("PER_APELLIDO", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Apellido);
                dynamicParameter.Add("PER_DIRECCION", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Direccion);
                dynamicParameter.Add("PER_TELEFONO", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Telefono);
                dynamicParameter.Add("PER_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input, personaRequest.Email);
               

                await _oracleRepository.Update(dynamicParameter, SpRecursoUsuarioLicencias.USP_ACTUALIZA_PERSONA);

            }
            catch
            {
                throw;
            }

        }


    }
}
