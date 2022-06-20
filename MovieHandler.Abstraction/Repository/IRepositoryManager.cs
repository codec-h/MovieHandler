using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHandler.Abstraction.Repository
{
    public interface IRepositoryManager
    {
        Task<bool> Insert<T>(string query, List<T> dataList);
        Task<bool> Insert<T>(string query, T data);

        Task<bool> ExecuteQuery(string query);
        Task<bool> ExecuteQuery(string query, object data);
        Task<int> ExecuteQueryWithRowsAffected(string query);

        Task<T> Fetch<T>(string query, object parameter);
        Task<List<T>> FetchWithNestedObjects<T, U>(string query, string key, string property, object parameter, string keyToGroupData);
        Task<List<T>> FetchBulk<T>(string query, object parameter);
        Task<bool> FetchScalar(string query, object parameter);

        Task<bool> Update<T>(string query, T data);
        Task<bool> Update<T>(string query, List<T> dataList);

        Task<bool> Delete<T>(string query, T data);
        Task<bool> Delete<T>(string query, List<T> dataList);
    }
}
