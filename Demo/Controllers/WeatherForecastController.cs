using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<WeatherForecast> Forecasts = new()
        {
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Ấm áp (Warm)" },
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)), TemperatureC = 8, Summary = "Lạnh giá (Chilly)" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> GetAll()
        {
            return Ok(Forecasts);
        }

        [HttpGet("{index}")]
        public ActionResult<WeatherForecast> GetByIndex(int index)
        {
            if (index < 0 || index >= Forecasts.Count)
            {
                return NotFound(new { Message = $"Không tìm thấy dự báo ở vị trí {index}." });
            }
            return Ok(Forecasts[index]);
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Create([FromBody] WeatherForecast newForecast)
        {
            if (newForecast == null)
            {
                return BadRequest(new { Message = "Dữ liệu gửi lên không hợp lệ." });
            }

            Forecasts.Add(newForecast);
            return CreatedAtAction(nameof(GetByIndex), new { index = Forecasts.Count - 1 }, newForecast);
        }
    }
}
