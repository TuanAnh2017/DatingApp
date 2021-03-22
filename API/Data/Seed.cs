using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return; // If we continue, that means we do not have any users in our database

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.Json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            //=> At this stage, our user should be a normal list of users of type app user
            // We should have it out of the json file by this point

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("0987"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user); // We don't need await this method, because we're adding, all we doing, don't forget
                                         // is tracking, adding, tracking to the user throught etity framework
                                         // We are not doing anything with the database at this point
                                         // => Đang add các user vào context nên chưa save vào database => Chưa sử dụng hàm await
            }
            await context.SaveChangesAsync();
        }
    }
}