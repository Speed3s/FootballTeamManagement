using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FootballTeamManagement.Models;
using FootballTeamManagement.ViewModels;


namespace FootballTeamManagement.Services
{
    public class FootballDataService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "ba82252dc7e24ee08aea53d08027fc1e";

        public FootballDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", ApiKey);
        }

        public async Task<string> GetArsenalResults()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://api.football-data.org/v2/teams/57/matches?status=FINISHED");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return content ?? "";  // Return an empty string if content is null
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetArsenalResults: {ex.Message}");
                return "";
            }
        }

        public async Task<string> GetArsenalFixtures()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://api.football-data.org/v2/teams/57/matches?status=SCHEDULED&limit=5");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetArsenalFixtures: {ex.Message}");
                return "";
            }
        }

        
    }
}
