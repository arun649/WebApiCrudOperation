using DemoWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options) 
        {
            
        }
        public DbSet<Registration> registrations { get; set; }
    }
}
