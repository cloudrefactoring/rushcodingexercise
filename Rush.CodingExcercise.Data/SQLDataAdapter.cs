using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Rush.CodingExercise.Data;

public class SQLDataAdapter : ISQLDataAdapter
{
    private readonly IConfiguration _config;

    public SQLDataAdapter(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> Query<T, U>(string storeProcedure, U parameters, string defaultConnection = "RushEnterprises")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(defaultConnection));

        var data = await connection.QueryAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
        return data;
    }
    public async Task<int> Create<P>(string storeProcedure, P parameters, string defaultConnection = "RushEnterprises")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(defaultConnection));

        var data = await connection.ExecuteAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
        return data;
    }

    public async Task<T> Create<T, P>(string storeProcedure, P parameters, string defaultConnection = "RushEnterprises")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(defaultConnection));

        var data = await connection.QueryAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
        return data.First();
    }

     public async Task<T> Update<T,P>(string storeProcedure, P parameters, string defaultConnection = "RushEnterprises")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(defaultConnection));

        var data = await connection.QueryAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
        return data.First();
    }
}
