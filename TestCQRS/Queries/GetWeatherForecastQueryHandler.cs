using System.Formats.Asn1;
using TestCQRS.CqrsUtils;

namespace TestCQRS.Queries
{
    public class GetWeatherForecastQueryHandler : QueryBase<GetWeatherForecastQueryHandler,IEnumerable<WeatherForecast>, Unit>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override Task<IEnumerable<WeatherForecast>> Handle(Unit request)
        {
            var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                Summaries[Random.Shared.Next(Summaries.Length)]
            ))
            .ToArray();

            return Task.FromResult<IEnumerable<WeatherForecast>>(forecast);
        }
    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
