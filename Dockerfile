# Bước 1: Biên dịch ứng dụng (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Sao chép tệp cấu hình dự án để khôi phục các thư viện (dependencies)
COPY Demo/Demo.csproj ./Demo/
RUN dotnet restore Demo/Demo.csproj

# Sao chép toàn bộ mã nguồn còn lại và xuất bản ứng dụng (publish)
COPY . .
RUN dotnet publish Demo/Demo.csproj -c Release -o out

# Bước 2: Khởi chạy ứng dụng (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Cấu hình cổng chạy ứng dụng ASP.NET Core
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Demo.dll"]
