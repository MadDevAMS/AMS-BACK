using System.Data;
using AMS.Infrastructure.Persistence.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Services
{
    public abstract class BaseRepository(ApplicationDbContext context)
    {
        protected readonly string _connectionString = context.Database.GetConnectionString()!;
        protected readonly ApplicationDbContext _context = context;

        protected int Execute(string sp, DynamicParameters? parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Execute(sp, parms, null, commandType: commandType);
        }

        protected List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        protected T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault()!;
        }

        protected async Task<int> ExecuteAsync(string sp, DynamicParameters? parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var response = await db.ExecuteAsync(sp, parms, null, commandType: commandType);
            return response;
        }

        protected async Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var response = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return response.ToList();
        }

        protected async Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var response = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return response.FirstOrDefault()!;
        }

        protected async Task<(IEnumerable<T1>, IEnumerable<T2>)> GetAllMultipleAsync<T1, T2>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var multi = await db.QueryMultipleAsync(sp, parms, commandType: commandType);

            var result1 = await multi.ReadAsync<T1>();
            var result2 = await multi.ReadAsync<T2>();

            return (result1, result2);
        }
    }
}
