using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DistributedRedisCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDistributedCache _distributedCache;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var cache = await _distributedCache.GetStringAsync("key");

            if(cache == null)
            {
                var value = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    })
                    .ToArray();

                await _distributedCache.SetStringAsync(
                    key: "key",
                    value: JsonSerializer.Serialize(value),
                    options:new DistributedCacheEntryOptions()
                    {
                        SlidingExpiration = TimeSpan.FromSeconds(30),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });

                return value;
            }

            return JsonSerializer.Deserialize<WeatherForecast[]>(cache);
        }
    }
}