using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetTest.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapperSession _session;

        public UserRepository(IMapperSession session)
        {
            _session = session;
        }
        public async Task<bool> UserExistsAsync(string iin)
        {
            return await _session.AppUsers.AnyAsync(x => x.Iin == iin);
        }

        public async Task<AppUser> GetUserByIdAsync(Guid id)
        {
            return await _session.AppUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _session.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetUserByIinAsync(string iin)
        {
            return await _session.AppUsers.Where(u => u.Iin == iin).FirstOrDefaultAsync();
        }

        public async Task<AppUser> CreateUserAsync(AppUser user)
        {        
            _session.BeginTransaction();
            var newUser = new AppUser()
            {
                Iin = user.Iin,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate
            };

            await _session.Save(newUser);
            await _session.Commit();
            _session.CloseTransaction();

            return user;
        }

        public async Task UpdateUserAsync(AppUser user)
        {
            var targetedUser = await GetUserByIdAsync(user.Id);

            targetedUser.Iin = user.Iin;
            targetedUser.FirstName = user.FirstName;
            targetedUser.LastName = user.LastName;
            targetedUser.BirthDate = user.BirthDate;

            _session.BeginTransaction();
            await _session.Save(targetedUser);
            await _session.Commit();
            _session.CloseTransaction();
        }

        public async Task DeleteUserAsync(AppUser user)
        {
            _session.BeginTransaction();
            await _session.Delete(user);
            await _session.Commit();
            _session.CloseTransaction();
        }
    }
}
