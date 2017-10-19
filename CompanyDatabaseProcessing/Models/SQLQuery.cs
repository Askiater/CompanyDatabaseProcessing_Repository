using System.Collections.Generic;
using System.Data.SqlClient;

namespace CompanyDatabaseProcessing.Models
{
    public static class SqlQuery
    {
        public static List<Person> CreateQuery(string connString)
        {
            var table = new List<Person>();
            using (var databaseConnection = new SqlConnection(connString))
            {           
                var cmd = new SqlCommand("SELECT * from People", databaseConnection);
                databaseConnection.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    table.Add(new Person
                    {
                        first_name = reader.GetString(1),
                        second_name = reader.GetString(2),
                        last_name = reader.GetString(3),
                        id_dep = reader.GetInt32(4),
                        id_post = reader.GetInt32(5),
                    });
                }
            }
            return table;
        }
    }
}