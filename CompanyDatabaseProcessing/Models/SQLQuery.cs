using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;

namespace CompanyDatabaseProcessing.Models
{
    public static class SqlQuery
    {
        public static List<PersonView> ViewData(string procedureName, PersonView item, string connString)
        {
            var tableOfPerson = new List<PersonView>();
            using (var databaseConnection = new SqlConnection(connString))
            {
                var cmd = new SqlCommand(procedureName, databaseConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (item != null)
                {
                    #region Add Parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar));
                    cmd.Parameters["@first_name"].Value = item.first_name;
                    cmd.Parameters.Add(new SqlParameter("@second_name", SqlDbType.VarChar));
                    cmd.Parameters["@second_name"].Value = item.second_name;
                    cmd.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar));
                    cmd.Parameters["@last_name"].Value = item.last_name;
                    cmd.Parameters.Add(new SqlParameter("@name_dep", SqlDbType.VarChar));
                    cmd.Parameters["@name_dep"].Value = item.dep;
                    cmd.Parameters.Add(new SqlParameter("@name_post", SqlDbType.VarChar));
                    cmd.Parameters["@name_post"].Value = item.post;
                    #endregion
                }

                databaseConnection.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new SqlExecutionException("Data that you try to find doesn't exist :(");
                }
                while (reader.Read())
                {
                    tableOfPerson.Add(new PersonView
                    {
                        first_name = reader.GetString(0),
                        second_name = reader.GetString(1),
                        last_name = reader.GetString(2),
                        dep = reader.GetString(3),
                        post = reader.GetString(4),
                    });
                }
            }
            return tableOfPerson;
        }

        public static void ChangeData(string procedureName, PersonView item, string connString)
        {

            using (var databaseConnection = new SqlConnection(connString))
            {
                var cmd = new SqlCommand(procedureName, databaseConnection)             
                {
                   CommandType = CommandType.StoredProcedure
                };

                #region Add Parameters to procedure

                cmd.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar));
                cmd.Parameters["@first_name"].Value = item.first_name;
                cmd.Parameters.Add(new SqlParameter("@second_name", SqlDbType.VarChar));
                cmd.Parameters["@second_name"].Value = item.second_name;
                cmd.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar));
                cmd.Parameters["@last_name"].Value = item.last_name;
                cmd.Parameters.Add(new SqlParameter("@name_dep", SqlDbType.VarChar));
                cmd.Parameters["@name_dep"].Value = item.dep;
                cmd.Parameters.Add(new SqlParameter("@name_post", SqlDbType.VarChar));
                cmd.Parameters["@name_post"].Value = item.post;

                #endregion
                
                databaseConnection.Open();
                var result = cmd.ExecuteNonQuery();
                if (result == -1)
                {
                    throw new SqlExecutionException("Data that you try to change/delete doesn't exist :(");
                }
            }

        }

    }
}



//using System.Data.Entity;

//namespace CompanyDatabaseProcessing.Models
//{
//    public class TableContext : DbContext
//    {
//        public DbSet<Department> deps { get; set; }
//        public DbSet<Post> posts { get; set; }
//        public DbSet<Person> persons { get; set; }
//    }
//}

//namespace CompanyDatabaseProcessing.Models
//{
//    public class Post
//    {
//        public int id { get; set; }
//        public string name { get; set; }
//    }
//}
//namespace CompanyDatabaseProcessing.Models
//{
//    public class Department
//    {
//        public int id { get; set; }
//        public string name { get; set; }
//    }
//}
//namespace CompanyDatabaseProcessing.Models
//{
//    public class Person
//    {
//        public int id { get; set; }
//        public string first_name { get; set; }
//        public string second_name { get; set; }
//        public string last_name { get; set; }
//        public int id_dep { get; set; }
//        public int id_post { get; set; }
//    }
//}
