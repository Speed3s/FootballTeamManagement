using Microsoft.EntityFrameworkCore;
using FootballTeamManagement.Models; // Replace with your project's namespace

namespace FootballTeamManagement.Data // Replace with your project's namespace
{
    public class FootballTeamContext : DbContext
    {
        public FootballTeamContext(DbContextOptions<FootballTeamContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        // Add other DbSets for other entities
    }
}
