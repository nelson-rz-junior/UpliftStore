using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using UpliftStore.DataAccess.Data.Repository.IRepository;

namespace UpliftStore.DataAccess.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _context;

        private static string _connectionString;

        public SP_Call(ApplicationDbContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetDbConnection().ConnectionString;
        }

        public void Execute<T>(string procedureName, DynamicParameters parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();

            connection.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }

        public T Get<T>(string procedureName, DynamicParameters parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection.ExecuteScalar<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<T> GetAll<T>(string procedureName, DynamicParameters parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
