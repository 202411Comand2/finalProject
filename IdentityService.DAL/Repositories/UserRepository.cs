using IdentityService.DAL.Abstractions;
using IdentityService.Domain;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Repositories
{
	public class UserRepository : IUserRepository<User>
	{
		public IContextManager ContextManager { get; private set; }
        public UserRepository(IContextManager contextManager)
        {
			ContextManager = contextManager;
        }
        public async Task<User> Add(User entity, CancellationToken? cancellationToken = null)
		{
			using(var context = ContextManager.CreateDatabaseContext())
			{
				await context.Users.AddAsync(entity);

				await context.SaveChangesAsync();
			}
			return entity;
		}
		public async Task<bool> Delete(int id, CancellationToken? cancellationToken = null)
		{
			using(var context = ContextManager.CreateDatabaseContext())
			{
				var rowsAffected = await context.Users
					.Where(x => x.Id == id)
					.ExecuteDeleteAsync();

				return rowsAffected > 0;
			}
		}
		public async Task<User?> Get(int id, CancellationToken? cancellationToken = null)
		{
			var result = new User();
			using(var context = ContextManager.CreateDatabaseContext())
			{
				result = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
			}
			return result;
		}
		public async Task<IList<User>> GetAll(CancellationToken? cancellationToken = null)
		{
			using(var context = ContextManager.CreateDatabaseContext())
			{
				return await context.Users.ToListAsync();
			}
		}
		public async Task<bool> Update(User entity, CancellationToken? cancellationToken = null)
		{
			using(var context = ContextManager.CreateDatabaseContext())
			{
				try
				{
					var attachedEntity = context.Users.FindAsync(entity.Id);
					context.Entry(attachedEntity).CurrentValues.SetValues(entity);
					await context.SaveChangesAsync();
				}
				catch (Exception ex) { return false; }
			}
			return true;
		}
        public async Task<User?> GetByEmail(string email, CancellationToken? cancellationToken = null)
        {
            using (var context = ContextManager.CreateDatabaseContext())
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
        public async Task<User?> GetByPhone(string phone, CancellationToken? cancellationToken = null)
        {
            using (var context = ContextManager.CreateDatabaseContext())
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
        public async Task<bool> ChangePassword(int userId, string newPassword, CancellationToken? cancellationToken = null)
        {
            try
            {
                using (var context = ContextManager.CreateDatabaseContext())
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
