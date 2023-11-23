using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballTeamManagement.Data;
using FootballTeamManagement.Models;
using FootballTeamManagement.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTeamManagement.Tests
{
    [TestClass]
    public class PlayersControllerTests
    {
        private FootballTeamContext _context;
        private PlayersController _controller;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FootballTeamContext>()
                .UseInMemoryDatabase(databaseName: "FootballTeamManagementTest")
                .Options;

            _context = new FootballTeamContext(options);

            _context.Players.Add(new Player { PlayerName = "TestPlayer1", Position = "Forward", JerseyNumber = 9, GoalsScored = 5 });
            _context.Players.Add(new Player { PlayerName = "TestPlayer2", Position = "Midfielder", JerseyNumber = 8, GoalsScored = 3 });
            _context.SaveChanges();

            _controller = new PlayersController(_context, null);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfPlayers()
        {
            var result = await _controller.Index();

            Assert.IsNotNull(result);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<Player>;
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public async Task Create_Post_ValidPlayer_AddsPlayerAndRedirectsToIndex()
        {
            var newPlayer = new Player { PlayerName = "NewPlayer", Position = "Defender", JerseyNumber = 4, GoalsScored = 1 };

            var result = await _controller.Create(newPlayer);

            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.IsTrue(_context.Players.Any(p => p.PlayerName == "NewPlayer"));
        }

        [TestMethod]
        public async Task Edit_Post_ValidData_UpdatesPlayerAndRedirects()
        {
            var playerToUpdate = _context.Players.First();
            playerToUpdate.PlayerName = "Updated Name";

            var result = await _controller.Edit(playerToUpdate.PlayerId, playerToUpdate);

            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("Updated Name", _context.Players.First().PlayerName);
        }

        [TestMethod]
        public async Task DeleteConfirmed_ValidPlayerId_DeletesPlayerAndRedirects()
        {
            var playerToDelete = _context.Players.First();

            var result = await _controller.DeleteConfirmed(playerToDelete.PlayerId);

            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.IsFalse(_context.Players.Any(p => p.PlayerId == playerToDelete.PlayerId));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
