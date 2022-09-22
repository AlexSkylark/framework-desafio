using System.Data.SqlClient;

public interface IDapperBase
{
    Task<int> ExecuteAsync(SqlConnection connection, string sql);
    Task<int> ExecuteAsync(SqlConnection connection, string sql, object param);
    Task<IEnumerable<T>> QueryAsync<T>(SqlConnection connection, string sql);
    Task<IEnumerable<T>> QueryAsync<T>(SqlConnection connection, string sql, object param);
    Task<T> QuerySingleAsync<T>(SqlConnection connection, string sql);
    Task<T> QuerySingleAsync<T>(SqlConnection connection, string sql, object param);
}