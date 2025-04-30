using Microsoft.AspNetCore.Mvc;
using TestCQRS.Commands;
using TestCQRS.Queries;
using TestCQRS.CqrsUtils;

namespace TestCQRS.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await GetWeatherForecastQueryHandler.Create().Handle(Unit.Value);
        }

        [HttpPost("SetFavoritePlace")]
        public async Task<IActionResult> SetFavoritePlace([FromBody] SetFavoritePlaceCommand request)
        {
            // Manual creation since FavoritePlaceCommand requires a constructor parameter
            await SetFavoritePlaceCommandHandler.Create().Handle(request);

            return Ok(new { message = "Favorite place set!" });
        }

        [HttpGet("GetFavoritePlace")]
        public async Task<GetFavoritePlaceQueryResponse> GetFavoritePlace()
        {
            return await GetFavoritePlaceQueryHandler.Create().Handle(Unit.Value);
        }
    }
}