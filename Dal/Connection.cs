using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;

namespace Dal
{
    public class Connection : IConnection
    {
        private SqlConnection _connection;

        private IOptions<ConnectionStrings> _option;

        public Connection(IOptions<ConnectionStrings> option)
        {
            _option = option;
            _connection = new SqlConnection(_option.Value.DefaultConnection);
        }

        public SqlConnection Open()
        {
            if (_connection == null)
                throw new Exception("");
            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();
            return _connection;
        }

        public void Close()
        {
            if (_connection != null)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection?.Close();                
            }            
        }

        public void Dispose()
        {
            Close();
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }

        public static IConnection Create(IOptions<ConnectionStrings> option)
        {
            return new Connection(option);
        }

        public SqlCommand CreateCommand()
        {
            return Open().CreateCommand();
        }
    }
}
