using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Authorize] // Tất cả user trong class này giờ sẽ được protected with authorization
    public class UserController : BaseAPIColtroller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        // [AllowAnonymous] // Chấp nhận vô danh - Bài 89 bỏ tính năng này đi
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsersAsync());
            //Cách khác: //return  _context.Users.ToListAsync().Result;
        }

        // [Authorize] // Bài 89 bỏ tính năng này đi
        [HttpGet("{userName}")]
        public async Task<ActionResult<AppUser>> GetUser(string userName)
        {
            return await _userRepository.GetUserByUserNameAsync(userName);
        }

    }
}