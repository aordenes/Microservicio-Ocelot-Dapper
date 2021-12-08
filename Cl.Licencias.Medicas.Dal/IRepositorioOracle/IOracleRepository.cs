using Cl.Licencias.Medicas.Dal.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cl.Licencias.Medicas.Dal.IRepositorioOracle
{
    public interface IOracleRepository<T> where T : class
    {
        /// <summary>
        /// Metodo generico para Obtener un registro en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters">parametros a filtrar en la consulta</param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un tipo entidad a definir</returns>
        Task<T> Get(OracleDynamicParameters dynamicParameters, string oracleSp);

        /// <summary>
        /// Metodo generico para Obtener una lista de registros en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters"></param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un tipo entidad a definir</returns>
        Task<IEnumerable<T>> GetAll(OracleDynamicParameters dynamicParameters, string oracleSp);

        /// <summary>
        /// Metodo generico para Insertar un registro en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters"></param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un true or false</returns>
        Task<bool> Insert(OracleDynamicParameters dynamicParameters, string oracleSp);

        /// <summary>
        /// Metodo generico para Insertar una lista en BD de oracle
        /// </summary>
        /// <param name="lstDynamicParameters">lista de parametros dinamicos</param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un true or false</returns>
        Task<bool> InsertMany(IEnumerable<OracleDynamicParameters> dynamicParameters, string oracleSp);

        /// <summary>
        /// Metodo generico para Actualizar un registro en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters"></param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un true or false</returns>
        Task<bool> Update(OracleDynamicParameters dynamicParameters, string oracleSp);

        /// <summary>
        /// Metodo generico para eliminar un registro en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters">parametros a filtrar en la consulta</param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un true or false</returns>
        Task<bool> Delete(OracleDynamicParameters dynamicParameters, string oracleSp);


    }
}
