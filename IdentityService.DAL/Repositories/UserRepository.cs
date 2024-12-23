using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;
using IdentityService.DAL.Exceptions;

namespace IdentityService.DAL.Repositories
{
	public class UserRepository : IRepository<User>
	{
		private readonly IContextManager _contextManager;
        public UserRepository(IContextManager contextManager)
        {
			_contextManager = contextManager;
        }
        public async Task<User> Add(User entity)
		{
			using(var context = _contextManager.CreateDatabaseContext())
			{
				await context.Users.AddAsync(entity);

				await context.SaveChangesAsync();
			}
			return entity;
		}

		public async Task<bool> Delete(int entityId)
		{
			using(var context = _contextManager.CreateDatabaseContext())
			{
				var entity = await context.Users.FindAsync(entityId);

				if (entity != null)
				{
					context.Users.Remove(entity);

					await context.SaveChangesAsync();
					return true;
				}
				else return false;
			}
		}

		public async Task<User> Get(int entityId)
		{
			var result = new User();
			using(var context = _contextManager.CreateDatabaseContext())
			{
				var hashedUser = await context.Users.FindAsync(entityId);
				if (hashedUser == null) throw new EntityNotExistsException();
				else
				{
					result.Id = hashedUser.Id;
					result.Name = hashedUser.Name;
					result.Password = DecryptSensitiveData(hashedUser.Password);
					result.Phone = DecryptSensitiveData(hashedUser.Phone);
					result.Email = DecryptSensitiveData(hashedUser.Email);
					result.TelegramId = hashedUser.TelegramId;
					result.DateCreated = hashedUser.DateCreated;
				}
			}
			return result;
		}

		public Task<IList<User>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<User> SaveOrUpdate(User entity)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Update(User entity)
		{
			using(var context = _contextManager.CreateDatabaseContext())
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

		private string EncryptSensitiveData(string data)
		{
			throw new NotImplementedException();
		}
		private string DecryptSensitiveData(string data)
		{
			throw new NotImplementedException();
		}
	}
}
