using Microsoft.EntityFrameworkCore;

namespace CovidScrapper.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CovidStatistics> CovidStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=StatisticsDB;Trusted_Connection=True;");
        }
    }
}
