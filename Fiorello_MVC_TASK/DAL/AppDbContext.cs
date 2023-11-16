
using Fiorello_MVC_TASK.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC_TASK.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        DbSet<Product> Products { get; set; }
    }
}
