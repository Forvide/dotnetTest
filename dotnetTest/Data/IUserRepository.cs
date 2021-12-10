using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetTest.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIinAsync(string iin);
        Task<AppUser> CreateUserAsync(AppUser user);
        Task UpdateUserAsync(AppUser user);
        Task DeleteUserAsync(AppUser user);
        Task<bool> UserExistsAsync(string iin);
        Task<AppUser> GetUserByIdAsync(Guid id);
    }
}