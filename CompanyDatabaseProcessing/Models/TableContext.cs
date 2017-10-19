using System.Data.Entity;

namespace CompanyDatabaseProcessing.Models
{
    public class TableContext : DbContext
    {
        public DbSet<Department> deps { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Person> persons { get; set; }
    }
}
