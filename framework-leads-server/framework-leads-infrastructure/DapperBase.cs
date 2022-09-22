using Dapper;
using System.Data.SqlClient;

public class DapperBase : IDapperBase
{
    public async Task<IEnumerable<T>> QueryAsync<T>(SqlConnection connection, string sql)
    {
        return await connection.QueryAsync<T>(sql);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(SqlConnection connection, string sql, object param)
    {
        return await connection.QueryAsync<T>(sql, param);
    }

    public async Task<T> QuerySingleAsync<T>(SqlConnection connection, string sql)
    {
        return await connection.QuerySingleAsync<T>(sql);
    }

    public async Task<T> QuerySingleAsync<T>(SqlConnection connection, string sql, object param)
    {
        return await connection.QuerySingleAsync<T>(sql, param);
    }

    public async Task<int> ExecuteAsync(SqlConnection connection, string sql)
    {
        return await connection.ExecuteAsync(sql);
    }

    public async Task<int> ExecuteAsync(SqlConnection connection, string sql, object param)
    {
        return await connection.ExecuteAsync(sql, param);
    }
}