using Microsoft.EntityFrameworkCore;

namespace ActivityCenter.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Act> Acts { get; set; }
        public DbSet<Association> Associations { get; set; }
    }
}