using System.Data;

namespace Cl.Dal.ContextOracleDb
{
    public interface IFactoryConnection
    {
        void CloseConnection();

        IDbConnection GetConnection();
    }
}
