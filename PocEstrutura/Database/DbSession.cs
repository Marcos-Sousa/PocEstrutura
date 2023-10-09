using static Dapper.SqlMapper;
using System.Data;
using System.Data.SqlClient;

namespace PocEstrutura.Database
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
