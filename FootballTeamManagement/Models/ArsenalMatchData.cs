namespace FootballTeamManagement.Models
{
    public class Match
    {
        public int Id { get; set; }
        public Team? HomeTeam { get; set; }
        public Team? AwayTeam { get; set; }
        
    }

public class Team
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
}

public class ArsenalMatchData
{
    public List<Match>? Matches { get; set; }
}

}
