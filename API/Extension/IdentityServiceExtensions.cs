using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extension
{
    public static class IdentityServiceExtensions
    {
       public static IServiceCollection AddIdentityServices (this IServiceCollection services, IConfiguration config)
       {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true, //Server sẽ ký nhận cái token và ta cần thực sự xác nhận cái token là chính xác
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                            // 2 lệnh ở trên là để ta có thể xác thực Our User cùng với Valid token

                            ValidateIssuer = false,
                            ValidateAudience = false // 2 cái Validate này có nghĩa là The issue of the token is obviously our API server
                                                    // and Audience of the token is our Angular Application

                        };
                    }); 

                return services;
       }
    }
}