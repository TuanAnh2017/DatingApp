using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user); // the updates is not an async method, because all that's
        // going to do is update the tracking status
        //in entity framework to say that something has changed, but all of the other ones are tasks.
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int Id);
        Task<AppUser> GetUserByUserNameAsync(string UserName);

    }
}