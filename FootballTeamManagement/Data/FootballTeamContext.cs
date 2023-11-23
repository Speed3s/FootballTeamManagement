using Microsoft.EntityFrameworkCore;
using FootballTeamManagement.Models;

namespace FootballTeamManagement.Data 
{
    public class FootballTeamContext : DbContext
    {
        public FootballTeamContext(DbContextOptions<FootballTeamContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

    }
}
