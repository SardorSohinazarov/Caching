using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers
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
        private readonly IMemoryCache _memoryCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var value = _memoryCache.Get("key");

            if(value == null)
            {
                _memoryCache.Set(
                    key:"key",
                    value:Enumerable.Range(1, 5).Select(index => new WeatherForecast
                        {
                            Date = DateTime.Now.AddDays(index),
                            TemperatureC = Random.Shared.Next(-20, 55),
                            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                        }).ToArray(),
                    options:new MemoryCacheEntryOptions() 
                        { 
                            SlidingExpiration = TimeSpan.FromSeconds(5),
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                            Size = 1024
                        }
                );
            }

            return _memoryCache.Get("key") as WeatherForecast[];
        }
    }
}