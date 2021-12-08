using System.Data;

namespace Cl.Licencias.Medicas.Dal.ContextOracleDb
{
    public interface IFactoryConnection
    {
        void CloseConnection();

        IDbConnection GetConnection();
    }
}
