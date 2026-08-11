# Hướng dẫn Thuyết trình & Giải thích Chi tiết Mã nguồn Dự án (Buổi 10)

Tài liệu này được chia thành 2 phần chính: **Kịch bản Thuyết trình/Demo (Tự động hóa hoàn toàn trên Cloud)** và **Giải thích chi tiết tác dụng của từng file và dòng code**.

---

## PHẦN 1: KỊCH BẢN THUYẾT TRÌNH DEMO (Tự động hóa Cloud)

### Bước 1: Mở đầu & Giới thiệu mục tiêu (Khoảng 1 phút)
* **Hành động**: Chiếu màn hình cấu trúc thư mục của dự án trên VS Code.
* **Lời nói minh họa**:
  > *"Xin chào thầy/cô và các bạn. Hôm nay em xin phép demo sản phẩm thực hành Buổi 10 với mục tiêu: **Tự động hóa hoàn toàn quy trình CI/CD**. Lập trình viên chỉ cần viết code và đẩy lên GitHub, toàn bộ các bước kiểm thử, đóng gói Docker và triển khai lên máy chủ Azure sẽ được thực hiện tự động trên đám mây bằng GitHub Actions mà không cần thao tác thủ công ở máy cá nhân."*

### Bước 2: Chỉnh sửa mã nguồn & Kích hoạt Pipeline (Khoảng 1.5 phút)
* **Hành động**: Thực hiện một chỉnh sửa nhỏ trong file [WeatherForecastController.cs](DEMO1/Demo/Controllers/WeatherForecastController.cs) (ví dụ: sửa thông tin chữ hoặc thêm ghi chú), sau đó gõ các lệnh Git trong Terminal để push:
  ```bash
  git add .
  git commit -m "demo: trigger automatic ci-cd pipeline"
  git push origin main
  ```
* **Lời nói minh họa**:
  > *"Em vừa thực hiện thay đổi nhỏ trong mã nguồn API của mình. Thay vì tự chạy kiểm thử hay tự gõ lệnh build Docker tốn tài nguyên dưới máy cá nhân, em chỉ cần thực hiện lệnh `git push` để đẩy code lên GitHub. Sự kiện này sẽ ngay lập tức kích hoạt hệ thống CI/CD chạy tự động."*

### Bước 3: Minh họa luồng chạy trên GitHub Actions (Khoảng 3 phút)
* **Hành động**: Mở trình duyệt Web, truy cập vào tab **Actions** trên repository GitHub của bạn:
  🔗 **[GitHub Actions Runs](https://github.com/0023411852-crypto/Demo/actions)**
  Bấm chọn vào workflow đang chạy để hiển thị sơ đồ 3 công việc (Jobs):
  1. **🔨 Build Project**: Đang cài đặt .NET và chạy `dotnet test`.
  2. **🐳 Docker Build & Push**: Đang build ảnh Docker từ `Dockerfile` và đẩy lên Docker Hub.
  3. **🚀 Deploy to Server**: Đang SSH vào VPS để triển khai.
* **Lời nói minh họa**:
  > *"Như mọi người thấy trên màn hình, GitHub Actions đã tự tạo ra các máy ảo độc lập để xử lý yêu cầu của em:
  > * Đầu tiên ở Job **Build**, hệ thống tự chạy kiểm thử tự động (Unit Test). Nếu bước này thất bại, quy trình sẽ dừng ngay lập tức để bảo vệ hệ thống.
  > * Sau khi kiểm thử thành công, Job **Docker Build** sẽ đóng gói ứng dụng thành Docker Image và đẩy lên Docker Hub.
  > * Cuối cùng, Job **Deploy** sử dụng kết nối SSH bảo mật để truy cập vào máy chủ Azure, kéo phiên bản mới về và kích hoạt chạy thông qua Docker Compose."*

### Bước 4: Kiểm tra kết quả thực tế trên Azure Server (Khoảng 1 phút)
* **Hành động**: Chờ cho Job Deploy báo xanh (Success). Sau đó mở tab trình duyệt mới và truy cập vào địa chỉ IP máy chủ Azure:
  👉 **[http://40.82.138.91:8080/weatherforecast](http://40.82.138.91:8080/weatherforecast)**
* **Lời nói minh họa**:
  > *"Sau khi cả 3 bước báo thành công, em tải lại trang web chạy trên địa chỉ IP máy chủ Azure cổng 8080. Kết quả API đã được cập nhật nội dung mới nhất. Quy trình CI/CD tự động hóa hoàn toàn của em đã hoạt động thành công xuất sắc. Em xin cảm ơn thầy cô và các bạn đã theo dõi."*

---

## PHẦN 2: CHI TIẾT CÁC FILE TRONG DỰ ÁN VÀ TÁC DỤNG CỦA CODE

### 1. File cấu hình dự án & Solution

#### 📄 [Demo.sln](DEMO1/Demo.sln) (Solution File)
* **Tác dụng**: Đây là file liên kết các dự án con lại với nhau trong một giải pháp (Solution). 
* **Tác dụng của code**: Khai báo dự án Web API chính (`Demo\Demo.csproj`) và dự án kiểm thử (`Demo.Tests\Demo.Tests.csproj`) để bạn có thể biên dịch hoặc chạy test cho cả hai dự án cùng một lúc chỉ với một câu lệnh.

#### 📄 [Demo/Demo.csproj](DEMO1/Demo/Demo.csproj) (Cấu hình Web API)
* **Tác dụng**: Khai báo phiên bản framework và các thư viện sử dụng.
* **Tác dụng của code**: 
  * `<TargetFramework>net8.0</TargetFramework>`: Chỉ định dự án chạy trên nền tảng .NET 8.0 mới nhất.
  * `<ImplicitUsings>enable</ImplicitUsings>`: Tự động import các namespace cơ bản của C# giúp code ngắn gọn hơn.

---

### 2. File Code xử lý API

#### 📄 [Demo/Program.cs](DEMO1/Demo/Program.cs) (Khởi động ứng dụng)
* **Tác dụng**: File đầu tiên được chạy khi ứng dụng khởi động. Nó khởi tạo Web Server và cấu hình các dịch vụ.
* **Tác dụng của code**:
  * `builder.Services.AddControllers()`: Cung cấp các dịch vụ để hệ thống hiểu và ánh xạ các Controller xử lý API.
  * `app.MapControllers()`: Ánh xạ trực tiếp các Endpoint từ Controller (như `/weatherforecast`) để người dùng bên ngoài có thể gọi được qua giao thức HTTP.

#### 📄 [Demo/Controllers/WeatherForecastController.cs](DEMO1/Demo/Controllers/WeatherForecastController.cs) (Xử lý Request)
* **Tác dụng**: Chứa các hàm xử lý logic khi client gọi vào API.
* **Tác dụng của code**:
  * `[ApiController]`: Tự động kiểm tra dữ liệu gửi lên (nếu sai định dạng sẽ tự trả về lỗi 400 BadRequest).
  * `[Route("[controller]")]`: Định nghĩa đường dẫn gọi API. Tên Controller là `WeatherForecastController` nên route truy cập sẽ là `/weatherforecast`.
  * `private static readonly List<WeatherForecast> Forecasts`: Cơ sở dữ liệu mẫu dạng danh sách (In-memory) để demo nhanh.
  * `[HttpGet] public ActionResult GetAll()`: Lấy toàn bộ danh sách thời tiết trả về dưới dạng JSON cùng mã thành công `200 OK`.

---

### 3. File Code kiểm thử (Unit Test)

#### 📄 [Demo.Tests/WeatherForecastControllerTests.cs](DEMO1/Demo.Tests/WeatherForecastControllerTests.cs) (Chạy Test)
* **Tác dụng**: Viết các kịch bản kiểm tra logic code của API xem có chạy đúng như kỳ vọng không.
* **Tác dụng của code**:
  * `[Fact]`: Đánh dấu hàm `LayToanBo_TraVeDanhSachKhongNullVaCoDuLieu` là một test case.
  * `var controller = new WeatherForecastController()`: Khởi tạo controller giả lập.
  * `Assert.IsType<OkObjectResult>(result.Result)`: Đảm bảo kết quả trả về từ API bắt buộc phải là mã HTTP 200 OK.
  * `Assert.NotEmpty(forecasts)`: Đảm bảo danh sách dữ liệu thời tiết trả về không được trống rỗng.

---

### 4. File Cấu hình Docker & Deployment

#### 📄 [Dockerfile](DEMO1/Dockerfile) (Đóng gói ứng dụng)
* **Tác dụng**: Tập hợp các bước để tạo thành một Container Image hoạt động độc lập không phụ thuộc hệ điều hành.
* **Tác dụng của code**:
  * **Stage 1 (Build)**: Sử dụng Image SDK nặng (`mcr.microsoft.com/dotnet/sdk:8.0`) để chạy lệnh biên dịch `dotnet publish` nhằm tối ưu hóa và xuất bản mã nguồn ra thư mục `/app/out`.
  * **Stage 2 (Runtime)**: Chuyển sang sử dụng Image Runtime siêu nhẹ (`mcr.microsoft.com/dotnet/aspnet:8.0`) và chỉ sao chép kết quả đã build từ Stage 1 sang để chạy. Giúp giảm dung lượng file ảnh Docker từ ~800MB xuống chỉ còn ~200MB.

#### 📄 [docker-compose.yml](DEMO1/docker-compose.yml) (Quản lý chạy container)
* **Tác dụng**: Cấu hình các tham số khi khởi chạy container trên máy chủ (Server Azure hoặc WSL 2).
* **Tác dụng của code**:
  * `ports: - "8080:8080"`: Ánh xạ cổng `8080` của máy chủ vào cổng `8080` của container, giúp người dùng bên ngoài truy cập được API qua địa chỉ IP của Server Azure.

---

### 5. File cấu hình GitHub Actions (CI/CD)

#### 📄 [.github/workflows/ci-cd.yml](DEMO1/.github/workflows/ci-cd.yml) (Tự động hóa luồng chạy)
* **Tác dụng**: Định nghĩa quy trình tự động hóa các bước kiểm thử, build và triển khai.
* **Tác dụng của code**:
  * `on: push: branches: [main]`: Kích hoạt quy trình tự động mỗi khi có code mới đẩy lên nhánh `main`.
  * `jobs: build`: Máy ảo chạy lệnh `dotnet test` tự động.
  * `jobs: docker-build`: Chạy lệnh `docker build-push-action` để build ảnh và đẩy lên Docker Hub.
  * `jobs: deploy`: Đăng nhập vào VPS bằng SSH thông qua các khóa bảo mật (Secrets), chạy lệnh kéo ảnh mới nhất từ Docker Hub về máy chủ Azure và khởi động lại container bằng Docker Compose.
