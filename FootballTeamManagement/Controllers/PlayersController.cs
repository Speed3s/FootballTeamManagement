using Microsoft.AspNetCore.Mvc;
using FootballTeamManagement.Data;
using FootballTeamManagement.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FootballTeamManagement.Services;
using FootballTeamManagement.ViewModels;

namespace FootballTeamManagement.Controllers
{
    public class PlayersController : Controller
    {
        private readonly FootballTeamContext _context;
        private readonly FootballDataService _footballDataService;

        public PlayersController(FootballTeamContext context, FootballDataService footballDataService)
        {
            _context = context;
            _footballDataService = footballDataService;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var players = await _context.Players.ToListAsync();
            return View(players);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,PlayerName,Position,JerseyNumber,GoalsScored")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,PlayerName,Position,JerseyNumber,GoalsScored")] Player player)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                // Handle the case where the player does not exist.
                // This could be a redirect to an error page or a NotFound result.
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ArsenalResults()
        {
            try
            {
                var results = await _footballDataService.GetArsenalResults();
                var fixtures = await _footballDataService.GetArsenalFixtures();

                var viewModel = new ArsenalResultsViewModel
                {
                    Results = results,
                    Fixtures = fixtures
                };

                return View("ArsenalResults", viewModel);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }


        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}
