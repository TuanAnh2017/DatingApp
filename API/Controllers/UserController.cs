using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Authorize] // Tất cả user trong class này giờ sẽ được protected with authorization
    public class UserController : BaseAPIColtroller
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        // [AllowAnonymous] // Chấp nhận vô danh - Bài 89 bỏ tính năng này đi
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
            //Cách khác: //return  _context.Users.ToListAsync().Result;
        }

        // [Authorize] // Bài 89 bỏ tính năng này đi
        [HttpGet("{id}")] 
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

    }
}