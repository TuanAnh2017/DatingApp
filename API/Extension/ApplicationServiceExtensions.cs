using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extension
{
    public static class ApplicationServiceExtensions
    {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
      {
           services.AddScoped<ITokenService, TokenService>(); // Lý do cho việc có interface ở đây là để gấp đôi, 1 cái để Testing
                                                               // Nó rất dễ cho việc bắt chước 1 interface
                                                               // Chúng ta chỉ cần chữ ký của phương thức sau đó ta bắt chước hành vi của nó
                                                               // khi test ứng dụng chúng ta
                                                               // Nó làm cho việc test ứng dụng của bạn sau đó dễ dàng hơn
                                                               // Lý do thứ 2: Chúng luôn sẵn có và chúng ta có thể test nó bất cứ khi nào ta muốn
                                                               // Nó chỉ làm việc tốt nếu bạn chỉ tạo ra TokenService triển khai từ nó

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
      }

    }
}