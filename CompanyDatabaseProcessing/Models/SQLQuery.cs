using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CompanyDatabaseProcessing.Models
{
    public static class SqlQuery
    {
        public static List<PersonView> GetAllData(string connString)
        {
            var tableOfPerson = new List<PersonView>();
            using (var databaseConnection = new SqlConnection(connString))
            {
                var cmd = new SqlCommand("GetAllValues", databaseConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                databaseConnection.Open();
                var reader = cmd.ExecuteReader();

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

        public static void AddItem(PersonView added, string connString)
        {

            using (var databaseConnection = new SqlConnection(connString))
            {
                var cmd = new SqlCommand("AddValue", databaseConnection)             
                {
                   CommandType = CommandType.StoredProcedure
                };
                #region Add Parameters to AddValue procedure

                cmd.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar));
                cmd.Parameters["@first_name"].Value = added.first_name;
                cmd.Parameters.Add(new SqlParameter("@second_name", SqlDbType.VarChar));
                cmd.Parameters["@second_name"].Value = added.second_name;
                cmd.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar));
                cmd.Parameters["@last_name"].Value = added.last_name;
                cmd.Parameters.Add(new SqlParameter("@name_dep", SqlDbType.VarChar));
                cmd.Parameters["@name_dep"].Value = added.dep;
                cmd.Parameters.Add(new SqlParameter("@name_post", SqlDbType.VarChar));
                cmd.Parameters["@name_post"].Value = added.post;

                #endregion
                
                databaseConnection.Open();
                var reader = cmd.ExecuteReader(); //????? reader?
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
