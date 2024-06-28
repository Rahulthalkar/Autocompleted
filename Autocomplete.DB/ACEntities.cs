using Autocomplete.Domain.Tables;
using Microsoft.EntityFrameworkCore;

namespace Autocomplete.DB
{
    public class ACEntities : DbContext
    {
        public ACEntities(DbContextOptions options) : base(options)
        {
        }
        public DbSet<tblUser> tblUsers { get; set; }

        public DbSet<tblUserType> tblUsersType { get; set; }

        public DbSet<Tableitem> tableItems { get; set; }
    }
}
