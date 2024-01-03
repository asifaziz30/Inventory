using Microsoft.Extensions.Configuration;

namespace Inventory.Service.Data.DbFactory
{
    public interface IDbFactoryBase
    {
        string ConnectionString { get; }
        DbFactory QueryExecutor { get; }
        IConfiguration Configuration { get; }
    }
    public class DbFactoryBase
    {
        private string connectionString;
        public DbFactoryBase(string connectionString, IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = connectionString;
        }

        public string ConnectionString
        {
            get => connectionString;
            set
            {
                connectionString = Configuration.GetConnectionString(value);
                QueryExecutor = new DbFactory(ConnectionString, Configuration);
            }
        }

        public DbFactory QueryExecutor { get; private set; }
        public IConfiguration Configuration { get; }
    }
}
