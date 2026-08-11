using Microsoft.AspNetCore.Mvc;
using Demo.Controllers;
using Xunit;

namespace Demo.Tests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void LayToanBo_TraVeDanhSachKhongNullVaCoDuLieu()
        {
            // 1. Chuẩn bị (Arrange)
            var controller = new WeatherForecastController();

            // 2. Thực hiện (Act)
            var result = controller.GetAll();

            // 3. Kiểm tra (Assert)
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var forecasts = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(okResult.Value);
            
            Assert.NotNull(forecasts);
            Assert.Empty(forecasts);
        }
    }
}
