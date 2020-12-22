using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
     
        public Startup(IConfiguration config)
        {
           this._config = config;
        }

     
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });
            services.AddControllers();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            // Trong hàm Congigure này việc sắp xếp thứ tự các câu lệnh là rất cần thiết
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200")); // Khi triển khai xác thực Implement Authentication => Nó phải nằm trước Authorization
            // x.AllowAnyHeader: Ta sẽ gửi các Header như là Header xác thực tới API từ Angular của chúng ta
            // AllowAnyMethod: Chúng ta chấp nhận mọi phương thức, chấp nhận Put request, Post request, Get request và v.v...
            // WithOrigins ("http:localhost:4200"): Bạn có thể làm bất cứ điều gì qua các phương thức nhưng phải đúng địa chỉ
            // Của Angular ở đây là localhost:4200

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
