using MySqlConnector;
using System.Data;

namespace qlts_api.Context
{
    public class DapperConnect
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        public DapperConnect(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DapperConnection");
        }
        public IDbConnection CreateConnection() => new MySqlConnection(connectionString);
    }
}
