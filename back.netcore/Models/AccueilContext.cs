using Microsoft.EntityFrameworkCore;


namespace back.net_core.Models
{
    public class AccueilContext : DbContext
    {
         public AccueilContext(DbContextOptions<AccueilContext> options):base(options){ }

        //Ajout des Entités gérées dans le contexte de persistance
        public DbSet<Visite> Visites { get; set; }
    }
}