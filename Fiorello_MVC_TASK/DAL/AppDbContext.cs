using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC_TASK.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
    }
}
