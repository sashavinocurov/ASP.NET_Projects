using Microsoft.EntityFrameworkCore;

namespace BeltPrep.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Happening> Happenings { get; set; }
        public DbSet<Association> Associations { get; set; }
    }
}