using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IspitDbContext : DbContext
    {
        // DbSet...
        public DbSet<Automobil> Automobili { get; set; }
        public DbSet<Boja> Boje { get; set; }
        public DbSet<Marka> Marke { get; set; }
        public DbSet<Model> Modeli { get; set; }
        public DbSet<Prodavnica> Prodavnice { get; set; }
        public DbSet<Spoj> Spojevi { get; set; }
        

        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
