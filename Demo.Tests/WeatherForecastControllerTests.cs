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
            // 1. Chuẩn bị (Arrange) - Khởi tạo controller cần kiểm thử
            var controller = new WeatherForecastController();

            // 2. Thực hiện (Act) - Gọi API lấy danh sách thời tiết
            var result = controller.GetAll();

            // 3. Kiểm tra (Assert) - Đảm bảo kết quả trả về đúng như mong đợi
            // Kiểm tra kết quả trả về có phải là mã HTTP 200 OK hay không
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            
            // Kiểm tra dữ liệu bên trong có đúng định dạng danh sách dự báo thời tiết không
            var forecasts = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(okResult.Value);
            
            // Đảm bảo danh sách không bị rỗng (null) và có chứa phần tử
            Assert.NotNull(forecasts);
            Assert.Empty(forecasts);
        }
    }
}
