using CadastroDeClientes.Domain.Context;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CadastroDeClientes.Infrastructure.Persistance.Context
{
    public class DapperContext<T> : IDapperContext<T>
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<Guid> Insert(string sql)
        {
            using var connection = CreateConnection();
            connection.Open();
            return await connection.QuerySingleOrDefaultAsync<Guid>(sql);
        }

        public async Task<bool> Update(string sql)
        {
            using var connection = CreateConnection();
            connection.Open();
            return await connection.ExecuteAsync(sql) > 1;
        }

        public async Task<bool> Delete(string sql)
        {
            using var connection = CreateConnection();
            connection.Open();
            return await connection.ExecuteAsync(sql) > 1;
        }
    }
}
