using System;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class BankaContext : DbContext
    {
        public DbSet<Korisnik> Korisnici { get; set;}
        public DbSet<Nalog> Nalozi { get; set; }

        public DbSet<Racun> Racuni { get; set; }
    
        public DbSet<VrsteRacuna> VrsteRacuna { get; set; }

        public BankaContext(DbContextOptions options) : base(options)
        {
            
        }

     

    }
}