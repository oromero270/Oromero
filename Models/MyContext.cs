using Microsoft.EntityFrameworkCore;
namespace Registration.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions options) :base(options){}

        public DbSet<Reg> Regs {get;set;}
        
    }
}