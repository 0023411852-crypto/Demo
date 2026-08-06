using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // Dữ liệu mẫu lưu trong bộ nhớ (Memory) để dễ dàng demo
        private static readonly List<WeatherForecast> Forecasts = new()
        {
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" },
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), TemperatureC = 30, Summary = "Hot" },
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), TemperatureC = 15, Summary = "Cool" },
           
        };

        // 1. Lấy toàn bộ danh sách dự báo thời tiết
        // GET /weatherforecast
        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> GetAll()
        {
            return Ok(Forecasts);
        }

        // 2. Lấy thông tin thời tiết theo index (vị trí trong danh sách)
        // GET /weatherforecast/{index}
        [HttpGet("{index}")]
        public ActionResult<WeatherForecast> GetByIndex(int index)
        {
            if (index < 0 || index >= Forecasts.Count)
            {
                return NotFound(new { Message = $"Không tìm thấy dự báo ở vị trí {index}." });
            }
            return Ok(Forecasts[index]);
        }

        // 3. Thêm mới một dự báo thời tiết
        // POST /weatherforecast
        [HttpPost]
        public ActionResult<WeatherForecast> Create([FromBody] WeatherForecast newForecast)
        {
            if (newForecast == null)
            {
                return BadRequest(new { Message = "Dữ liệu gửi lên không hợp lệ." });
            }

            Forecasts.Add(newForecast);
            
            // Trả về kết quả 201 Created kèm link đến API lấy chi tiết
            return CreatedAtAction(nameof(GetByIndex), new { index = Forecasts.Count - 1 }, newForecast);
        }
    }
}
