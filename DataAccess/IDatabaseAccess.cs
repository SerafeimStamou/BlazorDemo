using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDatabaseAccess
    {
        string ConnectionStringName { get; set; }

        Task<T> ManipulateData<T>(string query, T model);
        Task<T> FindById<T>(string query);
        Task<List<T>> ReadData<T>(string query, T model) where T : class;
    }
}