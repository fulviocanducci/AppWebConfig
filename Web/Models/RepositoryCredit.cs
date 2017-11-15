using System;
using Dal;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Web.Models
{
    public class RepositoryCredit : IRepositoryCredit
    {
        public IConnection Connection { get; }

        public RepositoryCredit(IConnection connection)
        {
            Connection = connection;
        }
        

        public bool Delete(Credit model)
        {
            return Delete(model.Id);
        }

        public bool Delete(object id)
        {
            bool result = false;
            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Credit WHERE Id=@Id";                
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
                result = (command.ExecuteNonQuery() > 0);
                Connection.Close();
            }
            return result;
        }

        public bool Edit(Credit model)
        {
            bool result = false;
            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "UPDATE Credit SET Description=@Description, Created=@Created WHERE Id=@Id";
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = model.Description;
                command.Parameters.Add("@Created", System.Data.SqlDbType.Date).Value = ((object)model.Created ?? DBNull.Value);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = model.Id;
                result = (command.ExecuteNonQuery() > 0);
                Connection.Close();
            }
            return result;
        }

        public Credit Find(object id)
        {
            Credit model = null;
            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "SELECT Id,Description,Created FROM Credit WHERE Id=@Id";
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        model = new Credit
                        {
                            Id = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            Created = reader.IsDBNull(2) ? default(DateTime?) : reader.GetDateTime(2)
                        };
                    }
                    reader.Close();               
                }
                Connection.Close();
            }
            return model;
        }

        public Credit Insert(Credit model)
        {
            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Credit(Description,Created) VALUES(@Description,@Created);SELECT CAST(scope_identity() AS INT)";
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = model.Description;
                command.Parameters.Add("@Created", System.Data.SqlDbType.Date).Value = ((object)model.Created ?? DBNull.Value);
                model.Id = (int)command.ExecuteScalar();
                Connection.Close();
            }
            return model;
        }

        public IEnumerable<Credit> List()
        {            
            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "SELECT Id,Description,Created FROM Credit ORDER BY Id ASC";                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Credit
                            {
                                Id = reader.GetInt32(0),
                                Description = reader.GetString(1),
                                Created = reader.IsDBNull(2) ? default(DateTime?) : reader.GetDateTime(2)
                            };
                        }
                    }
                    reader.Close();
                }
                Connection.Close();
            }            
        }
    }
}
