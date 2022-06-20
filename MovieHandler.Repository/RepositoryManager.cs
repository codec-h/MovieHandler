using Dapper;
using MovieHandler.Abstraction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHandler.Repository
{
    public class RepositoryManager
    {
        public class RepositoryManager : IRepositoryManager
        {
            string ConnectionString { get; set; } = string.Empty;

            private SqlConnection GetConnection()
            {
                SqlMapper.Settings.CommandTimeout = 0;
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("Connection string has not been configured yet!");
                else
                    return new SqlConnection(ConnectionString);
            }
            public async Task<T> Fetch<T>(string query, object parameter)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    T returnValue = await sqlConnection.QuerySingleAsync(query, parameter);
                    sqlConnection.Close();
                    return returnValue;
                }
            }
            public async Task<List<T>> FetchBulk<T>(string query, object parameter)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    var returnListValue = await sqlConnection.QueryAsync<T>(query, parameter);
                    sqlConnection.Close();
                    return returnListValue.ToList();
                }
            }
            public async Task<List<dynamic>> FetchDynamic(string query, object parameter)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    dynamic returnListValue = await sqlConnection.QueryAsync<dynamic>(query, parameter);
                    sqlConnection.Close();
                    return returnListValue;
                }
            }
            public async Task<List<T>> FetchWithNestedObjects<T, U>(string query, string splitOnKey, string property, object parameter, string keyToGroupData)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    List<T> parentList = new List<T>();
                    List<T> returnListValue = (await sqlConnection.QueryAsync<T, U, T>(query, (parent, child) =>
                    {
                        string parentKey = Convert.ToString(parent.GetType().GetProperty(keyToGroupData).GetValue(parent));
                        if (parentList.FindIndex(_ => Convert.ToString(_.GetType().GetProperty(keyToGroupData).GetValue(_)).Equals(parentKey)) == -1)
                        {
                            parent.GetType().GetProperty(property).SetValue(parent, new List<U>());
                            parentList.Add(parent);
                        }

                        var existingParent = parentList.First(_ => Convert.ToString(_.GetType().GetProperty(keyToGroupData).GetValue(_)).Equals(parentKey));
                        ((List<U>)existingParent.GetType().GetProperty(property).GetValue(existingParent)).Add(child);

                        return parent;
                    }, parameter,
                     splitOn: splitOnKey)).ToList();
                    sqlConnection.Close();
                    return parentList;
                }
            }
            public async Task<bool> FetchScalar(string query, object parameter)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    bool returnValue = Convert.ToBoolean(await sqlConnection.ExecuteScalarAsync(query, parameter));
                    sqlConnection.Close();
                    return returnValue;
                }
            }


            public async Task<bool> Insert<T>(string query, T data)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query, data);
                    sqlConnection.Close();
                    if (numberOfRowsAffected > 0)
                        return true;
                }
                return false;
            }
            public async Task<bool> Insert<T>(string query, List<T> dataList)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query, dataList);
                    sqlConnection.Close();
                    if (numberOfRowsAffected > 0)
                        return true;
                }
                return false;
            }


            public async Task<bool> Update<T>(string query, T data)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query, data);
                    sqlConnection.Close();
                    if (numberOfRowsAffected > 0)
                        return true;
                }
                return false;
            }
            public async Task<bool> Update<T>(string query, List<T> dataList)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query, dataList);
                    sqlConnection.Close();
                    if (numberOfRowsAffected > 0)
                        return true;
                }
                return false;
            }


            public async Task<bool> Delete<T>(string query, T data)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query, data);
                    sqlConnection.Close();
                    if (numberOfRowsAffected > 0)
                        return true;
                }
                return false;
            }
            public async Task<bool> Delete<T>(string query, List<T> dataList)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query, dataList);
                    sqlConnection.Close();
                    if (numberOfRowsAffected > 0)
                        return true;
                }
                return false;
            }


            public async Task<bool> ExecuteQuery(string query)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query);
                    sqlConnection.Close();
                    if (numberOfRowsAffected > 0)
                        return true;
                    return false;
                }
            }
            public async Task<bool> ExecuteQuery(string query, object data)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query, data);
                    sqlConnection.Close();
                    if (numberOfRowsAffected > 0)
                        return true;
                    return false;
                }
            }
            public async Task<int> ExecuteQueryWithRowsAffected(string query)
            {
                using (SqlConnection sqlConnection = GetConnection())
                {
                    sqlConnection.Open();
                    int numberOfRowsAffected = await sqlConnection.ExecuteAsync(query);
                    sqlConnection.Close();
                    return numberOfRowsAffected;
                }
            }
        }
    }
