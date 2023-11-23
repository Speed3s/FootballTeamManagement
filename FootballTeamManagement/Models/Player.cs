namespace FootballTeamManagement.Models
{
    public class Player
    {
        public Player()
        {
            PlayerName = string.Empty;
            Position = string.Empty;
        }
        public int PlayerId { get; set; }
        public string? PlayerName { get; set; }
        public string? Position { get; set; }
        public int JerseyNumber { get; set; }
        public int GoalsScored { get; set; }
    }
}
