using Microsoft.AspNetCore.Mvc;
using FootballTeamManagement.Services;

[ApiController]
[Route("[controller]")]
public class FootballDataController : ControllerBase
{
    private readonly FootballDataService _footballDataService;

    public FootballDataController(FootballDataService footballDataService)
    {
        _footballDataService = footballDataService;
    }

    [HttpGet("arsenal/results")]
    public async Task<IActionResult> GetArsenalResults()
    {
        try
        {
            var results = await _footballDataService.GetArsenalResults();
            return Ok(results); 
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

}
