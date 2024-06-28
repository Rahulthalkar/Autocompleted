using Autocomplete.Domain.Tables;
using Microsoft.EntityFrameworkCore;

namespace Autocomplete.DB
{
    public class ACEntities : DbContext
    {
        public string ConnectionString { get; }

        public ACEntities(string connectionString)
        {
            ConnectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public ACEntities(DbContextOptions options) : base(options)
        {
        }

        

        public DbSet<tblUser> tblUsers { get; set; }

        public DbSet<tblUserType> tblUsersType { get; set; }

        public DbSet<Tableitem> tableItems { get; set; }
    }
}
