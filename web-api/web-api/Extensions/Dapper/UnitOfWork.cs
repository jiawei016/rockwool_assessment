using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using thirdparty_api;

namespace web_api.Extensions.Dapper
{
    public class UnitOfWork
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            return _connection;
        }
        public IDbTransaction GetTransaction()
        {
            return _transaction;
        }

        public void OpenConnection()
        {
            _connection = new SqlConnection(_configuration.GetConnectionString("MSSQLConnection"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            _connection.Dispose();
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _connection.Dispose();
            _transaction.Dispose();
        }
    }
}
