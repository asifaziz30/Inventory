using System.Data;
using Dapper;
using KendoUI = Kendo.Mvc.UI;
using Microsoft.Extensions.Configuration;
using Inventory.Service.Models.DTO.Response;
using MySqlConnector;


namespace Inventory.Service.Data.DbFactory
{
    public class DbFactory
    {
        private readonly IConfiguration configuration;
        public string ConnectionString { get; set; } 
        public DbFactory(string connectionString, IConfiguration configuration)
        {
            ConnectionString = "server=localhost;port=3306;Database=inventory;User Id=root;Password=root!1234;";//connectionString;
            this.configuration = configuration;
            //Settings.CommandTimeout = 0;
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = "")
        {
            try
            {
                connection = String.IsNullOrEmpty(connection) ? ConnectionString : configuration.GetConnectionString(connection);
                using (var conn = new MySqlConnection(connection))
                {
                    conn.Open();
                    return await conn.QueryAsync<T>(sql, param, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = "")
        {
            try
            {
                connection = String.IsNullOrEmpty(connection) ? ConnectionString : configuration.GetConnectionString(connection);
                using (var conn = new MySqlConnection(connection))
                {
                    conn.Open();
                    return await conn.QuerySingleAsync<T>(sql, param, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public async Task<Tuple<T, IEnumerable<T1>>> QuerySingleAsync<T, T1>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = "")
        {
            try
            {
                connection = String.IsNullOrEmpty(connection) ? ConnectionString : configuration.GetConnectionString(connection);
                using var conn = new MySqlConnection(connection);
                conn.Open();
                var reader = await conn.QueryMultipleAsync(sql, param, commandType: commandType);
                var item = reader.Read<T>().FirstOrDefault();
                var items = reader.Read<T1>().ToList();
                return new Tuple<T, IEnumerable<T1>>(item, items);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<KendoUI.DataSourceResult> SPQuerySourceResultAsync<T>(string sql, object? param = null, string? connection = null, KendoUI.DataSourceRequest? sourceRequest = null)
        {
            try
            {
                connection = String.IsNullOrEmpty(connection) ? ConnectionString : configuration.GetConnectionString(connection);
                using var conn = new MySqlConnection(connection);
                conn.Open();
                var reader = await conn.QueryMultipleAsync(sql, param, commandType: CommandType.StoredProcedure);
                var items = reader.Read<T>().ToList();
                var count = reader.Read<int>().FirstOrDefault();
                return new KendoUI.DataSourceResult
                {
                    Data = items,
                    Total = count
                };
            }
             catch (Exception ex)
            {
                return null;
            }
        }

    }
}
