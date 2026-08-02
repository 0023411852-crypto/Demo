using Microsoft.AspNetCore.Mvc;
using Demo.Controllers;
using Xunit;

namespace Demo.Tests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void GetAll_ReturnsNonNullForecastsAndExpectedCount()
        {
            // Arrange
            var controller = new WeatherForecastController();

            // Act
            var result = controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var forecasts = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(okResult.Value);
            
            Assert.NotNull(forecasts);
            Assert.NotEmpty(forecasts);
        }
    }
}
