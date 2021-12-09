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
    public class AccesoContrato<T> : IAccesoContrato<T> where T : class
    {
        private readonly IOracleRepository<T> _oracleRepository;

        public AccesoContrato(IOracleRepository<T> oracleRepository)
        {
            _oracleRepository = oracleRepository;
        }

        /// <summary>
        /// Obtengo informacion del usuario a partir del nombre y el password
        /// </summary>
        /// <param name="authenticationRequest">entidad de entrada, entregada por la API de accesos</param>
        /// <returns>una entidad de tipo persona</returns>
        public async Task<T> GetPersonaByNombreAndPass(AuthenticationRequest authenticationRequest)
        {
            
            T persona;
            try
            {
                var dynamicParameter = new OracleDynamicParameters();
                dynamicParameter.Add("PERSONACURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                dynamicParameter.Add("NOMBRE_PERSONA", OracleDbType.Varchar2, ParameterDirection.Input, authenticationRequest.Nombre);
                dynamicParameter.Add("PASSUSER_PERSONA", OracleDbType.Varchar2, ParameterDirection.Input, authenticationRequest.Password);

                persona = await _oracleRepository.Get(dynamicParameter, SpRecursosAccesos.USP_GET_PERSONA_NOMBRE);



            }
            catch 
            {
                throw;
            }


            return persona;
        }

        /// <summary>
        /// Obtiene a la persona por medio del ID
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>Retorna a la persona</returns>
        public async Task<T> GetPersonaId(int? id)
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
    }
}
