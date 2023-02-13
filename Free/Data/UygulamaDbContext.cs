using Microsoft.EntityFrameworkCore;

namespace Free.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }



        public DbSet<Kullanici> Kullanicilar => Set<Kullanici>();
        public DbSet<Post> Posts => Set<Post>();    
    }
}
