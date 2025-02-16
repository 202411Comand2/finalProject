using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IContextManager manager) : base(manager)
        {

        }

        public async Task<User> GetByEmail(string email)
        {
            using(var context = CreateDatabaseContext())
            {
                return await context.Users.Where(u => u.IsDeleted == false)
                    .FirstOrDefaultAsync(x => x.Email == email);
            }
        }
        public async Task<User> GetByPhone(string phone)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Users.Where(u => u.IsDeleted == false)
                    .FirstOrDefaultAsync(x => x.Phone == phone);
            }
        }
	}
}
