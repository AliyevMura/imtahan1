
using Bilet_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Bilet_1.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt) { }





        public  DbSet<Post> Posts { get; set; }
    }
}
