
using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IContextManager manager) : base(manager)
        {
            
        }

        //public override async Task<User> Add(User entity)
        //{
        //    // return base.Add(entity);
        //    using (var context = CreateDatabaseContext()) 
        //    {
        //        var itemUser = await context.Users.AddAsync(entity);
        //        await context.SaveChangesAsync();
        //    }
        //    return entity;
        //}

    }

   public class TestRepository 
    {
        private UserRepository _userRepository;

        public TestRepository() 
        {
            var sn = new ContextManager();
            _userRepository = new UserRepository(sn);
        }

        public async Task <User> GetUser(int id)
        {
           return  await _userRepository.Get(id);
        }
    }
}
