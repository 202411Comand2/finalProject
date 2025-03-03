using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository(IContextManager manager) : BaseRepository<User>(manager)
    {
        public async Task<User?> GetByEmail(string email)
        {
            using(var context = CreateDatabaseContext())
            {
                return await context.Users.Where(u => u.IsDeleted == false && u.Email == email)
                    .Select(x => new User
                    {
                        Id = x.Id,
                        Password = x.Password,
                    })
                    .FirstOrDefaultAsync();
            }
        }
        public async Task<User?> GetByPhone(string phone)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Users.Where(u => u.IsDeleted == false && u.Phone == phone)
                    .Select(x => new User
                    {
                        Id = x.Id,
                        Password = x.Password,
                    })
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<bool> ChangePassword(int userId, byte[] newPassword)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    var tempUser = new User { Id = userId };
                    context.Attach(tempUser);
                    tempUser.Password = newPassword;
                    context.Entry(tempUser).Property(x => x.Password).IsModified = true;
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception) { return false; }
        }
	}
}
