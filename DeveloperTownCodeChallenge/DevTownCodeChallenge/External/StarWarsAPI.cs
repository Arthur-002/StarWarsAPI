using Microsoft.VisualBasic;
using System.Text.Json;
using System.Linq;

namespace DevTownCodeChallenge.External;

public class StarWarsAPI
{
    private static string _GetStarshipsPath = "/api/starships";

    public async Task<StarshipList> GetAllStarships(int pageNumber)
    {
        var client = new HttpClient()
        {
            BaseAddress = new Uri("https://swapi.dev")
        };
        using HttpResponseMessage response = await client.GetAsync($"/api/starships/?page={pageNumber}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var starships = JsonSerializer.Deserialize<StarshipList>(jsonResponse);
        if (starships == null)
        {
            starships = new StarshipList();
        }

        return starships;
    }
    
    public async Task<StarshipList> GetAllStarshipsByManufacturer(string manufacturer, int pageNumber)
    {
        var client = new HttpClient()
        {
            BaseAddress = new Uri("https://swapi.dev")
        };
        using HttpResponseMessage response = await client.GetAsync($"/api/starships/?page={pageNumber}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var starships = JsonSerializer.Deserialize<StarshipList>(jsonResponse);
        if (starships == null)
        {
            starships = new StarshipList();
        }
        else
        {
            starships.results = starships.results.Where(
                ship => ship.manufacturer.Contains(manufacturer, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        return starships;
    }

    public async Task<Starship> GetSingleStarship(int starshipId)
    {
        var client = new HttpClient()
        {
            BaseAddress = new Uri("https://swapi.dev")
        };
        using HttpResponseMessage response = await client.GetAsync($"/api/starships/{starshipId}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var starship = JsonSerializer.Deserialize<Starship>(jsonResponse);
        if (starship == null)
        {
            starship = new Starship();
        }

        return starship;
    }
}