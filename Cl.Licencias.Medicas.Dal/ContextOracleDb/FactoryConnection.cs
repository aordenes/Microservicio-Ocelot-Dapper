using Cl.Licencias.Medicas.Dal.Helper;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Licencias.Medicas.Dal.ContextOracleDb
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;

        private readonly IOptions<ConexionConfiguracion> _configs;

        public FactoryConnection(IOptions<ConexionConfiguracion> configs)
        {
            _configs = configs;
        }

        public void CloseConnection()
        {
            if (_connection == null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new OracleConnection(_configs.Value.DefaultConnection);
            }
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }
    }
}
