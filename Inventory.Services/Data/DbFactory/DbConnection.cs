namespace Inventory.Service.Data.DbFactory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using MySqlConnector;

    public interface IDbConnection
    {
        public MySqlConnection GetConnection { get; }
    }
    public class DbConnection : IDbConnection
    {
        IConfiguration Configuration;

        public DbConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public MySqlConnection GetConnection
        {
            get
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                return new MySqlConnection(connectionString);
            }
        }
    }
}
