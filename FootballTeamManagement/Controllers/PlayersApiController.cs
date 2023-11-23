using Microsoft.AspNetCore.Mvc;
using FootballTeamManagement.Data;
using Microsoft.EntityFrameworkCore;
using FootballTeamManagement.Models;

[Route("api/[controller]")]
[ApiController]
public class PlayersApiController : ControllerBase
{
    private readonly FootballTeamContext _context;

    public PlayersApiController(FootballTeamContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
    {
        return await _context.Players.ToListAsync();
    }

}
