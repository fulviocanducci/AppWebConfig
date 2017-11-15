using System.Data.SqlClient;
using System;
namespace Dal
{
    public interface IConnection: IDisposable
    {
        void Close();
        SqlConnection Open();
        SqlCommand CreateCommand();
    }
}