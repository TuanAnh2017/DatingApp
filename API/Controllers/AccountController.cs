using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using System;

namespace API.Controllers
{
    public class AccountController : BaseAPIColtroller
    {
        private const string Value = "PassWord is Invalid";
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username)) return BadRequest("UserName is taken");

            using var hmac = new HMACSHA512(); // Nó sẽ cung cấp cho ta hàm băm để tạo password hash
            var user = new AppUser
            {
                UserName = registerDTO.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),  // Lỗi xuất hiện ở đây, Hàm ComputeHash Convert string ra
                                                                                                // ByteArraay mà Password is null nên lỗi, out ra ngoài
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            // bắt đầu theo dõi thực thể đã cho và bất kỳ thực thể có thể truy cập nào khác chưa được theo dõi
            await _context.SaveChangesAsync(); // Gọi database và save vào User table

            return new UserDto
            {
                Username = user.UserName,
                // Password = user.PasswordSalt.ToString(),
                Token = _tokenService.CreateToken(user)
            };

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.Username); // Cái này trả về phần tử duy nhất của chuỗi hoặc là giá trị mặc định nếu chuỗi là rỗng

            if (user == null) return Unauthorized("Invalid UserName");
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            try
            {
                for (int i = 0; i < ComputeHash.Length; i++)
                {
                    if (ComputeHash[i] != user.PasswordHash[i])
                    {
                        return Unauthorized(Value);
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return new UserDto
            {
                Username = user.UserName,
                // Password = user.PasswordSalt.ToString(),
                Token = _tokenService.CreateToken(user)
            };

        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower()); // Hàm này sẽ kiểm tra xem nếu có bất kì 1 User nào
                                                                                         // bên trong bảng mà phù hợp với username này


        }

    }
}