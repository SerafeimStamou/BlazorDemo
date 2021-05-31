using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "Default";

        public DatabaseAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> ReadData<T>(string query, T model) where T : class
        {
            string ConnectionString = _config.GetConnectionString(ConnectionStringName);

             using (SqlConnection conn = new (ConnectionString))
             {
               var data = await conn.QueryAsync<T>(query, model);
               return data.ToList();
             }   
           
        }

        public async Task<T> FindById<T>(string query)
        {
            string ConnectionString = _config.GetConnectionString(ConnectionStringName);

            using (SqlConnection conn = new(ConnectionString))
            {
              var data = await conn.QueryAsync<T>(query);
              return data.FirstOrDefault();
            }
        }

        public async Task<T> ManipulateData<T>(string query, T model)
        {
            string ConnectionString = _config.GetConnectionString(ConnectionStringName);

            using (SqlConnection conn = new (ConnectionString))
            {
                await conn.ExecuteAsync(query, model);
                return model;
            }
          
        }
    }
}
