using DevTownCodeChallenge.External;
using Microsoft.AspNetCore.Mvc;

namespace DevTownCodeChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class CodeChallengeController : ControllerBase
{
    private readonly ILogger<CodeChallengeController> _logger;

    public CodeChallengeController(ILogger<CodeChallengeController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [Route("GetAllStarships/{pageNumber}")]
    public async Task<StarshipList> GetAllStarships(int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation($"GetAllStarships called");
            var starWarsAPI = new StarWarsAPI();
            var starships = await starWarsAPI.GetAllStarships(pageNumber);

            _logger.LogInformation($"GetAllStarships returning {starships?.results?.Count} starships");
            return starships;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetAllStarships ERROR: {ex}");
            return new StarshipList()
            {
                error = $"An error occured when trying to retrieve the list of starships: {ex}"
            };
        }
    }
    
    [HttpGet]
    [Route("GetAllStarshipsByManufacturer/{manufacturer}/{pageNumber}")]
    public async Task<StarshipList> GetAllStarshipsByManufacturer(string manufacturer, int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation($"GetAllStarshipsByManufacturer called for manufacturer = {manufacturer}, pageNumber = {pageNumber}");
            var starWarsAPI = new StarWarsAPI();
            var starships = await starWarsAPI.GetAllStarshipsByManufacturer(manufacturer, pageNumber);

            _logger.LogInformation($"GetAllStarshipsByManufacturer returning {starships?.results?.Count} starships");
            return starships;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetAllStarshipsByManufacturer ERROR: {ex}");
            return new StarshipList()
            {
                error = $"An error occured when trying to retrieve the list of starships: {ex}"
            };
        }
    }

    [HttpGet]
    [Route("GetStarshipById/{starshipId}")]
    public async Task<Starship> GetStarshipById(int starshipId)
    {
        try
        {
            _logger.LogInformation($"GetStarshipById called with starshipId = {starshipId}");
            var starWarsAPI = new StarWarsAPI();
            var starships = await starWarsAPI.GetSingleStarship(starshipId);

            return starships;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetStarshipById ERROR: {ex}");
            return new Starship();
            // {
            //     error = $"An error occured when trying to retrieve the list of starships: {ex}"
            // };
        }
    }
}
