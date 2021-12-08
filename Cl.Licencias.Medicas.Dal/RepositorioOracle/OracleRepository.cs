using Cl.Licencias.Medicas.Dal.ContextOracleDb;
using Cl.Licencias.Medicas.Dal.Helper;
using Cl.Licencias.Medicas.Dal.IRepositorioOracle;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Licencias.Medicas.Dal.RepositorioOracle
{
    public class OracleRepository<T> : IOracleRepository<T> where T : class
    {
        private readonly IFactoryConnection _factoryConnection;

        public OracleRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }


        /// <summary>
        /// Metodo generico para Obtener un registro en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters">parametros a filtrar en la consulta</param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un tipo entidad a definir</returns>
        public async Task<T> Get(OracleDynamicParameters dynamicParameters, string oracleSp)
        {
           
            T resultEntidad = null;
            try
            {               

                using IDbConnection connection = _factoryConnection.GetConnection();
                resultEntidad = await connection.QueryFirstAsync<T>(oracleSp, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch 
            {
                throw;
            }
            finally
            {

                _factoryConnection.CloseConnection();
            }

            return resultEntidad;
        }

        /// <summary>
        /// Metodo generico para Obtener una lista de registros en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters"></param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un tipo entidad a definir</returns>
        public async Task<IEnumerable<T>> GetAll(OracleDynamicParameters dynamicParameters, string oracleSp)
        {
            
            IEnumerable<T> lstResultado = null;
            try
            {
               
                using IDbConnection connection = _factoryConnection.GetConnection();
                lstResultado = await connection.QueryAsync<T>(oracleSp, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch 
            {
                throw;
            }
            finally
            {

                _factoryConnection.CloseConnection();
            }

            return lstResultado;
        }

        /// <summary>
        /// Metodo generico para Insertar un registro en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters"></param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un true or false</returns>
        public async Task<bool> Insert(OracleDynamicParameters dynamicParameters, string oracleSp)
        {

            bool result = false;
            //using IDbConnection connection = _factoryConnection.GetConnection();
            //rsultado = await connection.ExecuteAsync(oracleSp, dynamicParameters, commandType: CommandType.StoredProcedure);
            try
            {
                using (IDbConnection connection = _factoryConnection.GetConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            await connection.ExecuteAsync(oracleSp, dynamicParameters, commandType: CommandType.StoredProcedure, transaction: transaction);                            
                            transaction.Commit();
                            result = true;

                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }                   
            }
            catch
            {
                throw;
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }     

            return result;
        }

        /// <summary>
        /// Metodo generico para Insertar una lista en BD de oracle
        /// </summary>
        /// <param name="lstDynamicParameters">lista de parametros dinamicos</param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un true or false</returns>
        public async Task<bool> InsertMany(IEnumerable<OracleDynamicParameters> lstDynamicParameters, string oracleSp)
        {

            bool result = false;
            try
            {

                using IDbConnection connection = _factoryConnection.GetConnection();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var dynamicParameters in lstDynamicParameters)
                        {
                            await connection.ExecuteAsync(oracleSp, dynamicParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                            transaction.Commit();
                            result = true;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }            
            }
            catch
            {
                throw;
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Metodo generico para Actualizar un registro en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters"></param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un true or false</returns>
        public async Task<bool> Update(OracleDynamicParameters dynamicParameters, string oracleSp)
        {
            
          
            bool result = false;
            try
            {

                using (IDbConnection connection = _factoryConnection.GetConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            await connection.ExecuteAsync(oracleSp, dynamicParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                            transaction.Commit();
                            result = true;

                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch 
            {
                throw;
            }
            finally
            {

                _factoryConnection.CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Metodo generico para eliminar un registro en BD de oracle
        /// </summary>
        /// <param name="dynamicParameters">parametros a filtrar en la consulta</param>
        /// <param name="oracleSp">nombre del procedimiento almacenado</param>
        /// <returns>Retorna un true or false</returns>
        public async Task<bool> Delete(OracleDynamicParameters dynamicParameters, string oracleSp)
        {

            bool result = false;

            try
            {

                using (IDbConnection connection = _factoryConnection.GetConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            await connection.ExecuteAsync(oracleSp, dynamicParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                            transaction.Commit();
                            result = true;

                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {

                _factoryConnection.CloseConnection();
            }

            return result;
        }
    }
}
