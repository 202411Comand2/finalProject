using Domain.Entities;
using DAL.Repositories;
using DAL;

namespace Test
{
    internal class Program
    {
       static  UserRepository UserRepository { get; set; } =  new UserRepository(new ContextManager());

        static async Task Main(string[] args)
        {
          AddNewUser();
        }

        static async public Task AddNewUser()
        {
                Cart cart = new Cart()
                {
                };

                AuthToken authToken = new AuthToken()
                {
                    TokenId = new Guid(),
                    Device = "phone",
                    ExpireDate = DateTime.UtcNow.AddDays(1),
                };

                Random random = new Random();
                User person = new User()
                {
                    Name = $"Oleg{random.Next(0, 1000)}",
                    //Password = "1234Pasword",
                    Phone = "71234567899",
                    Email = "testEmail@Yndex.ru",
                    TelegramId = 0,
                    AuthToken = authToken,

                };
                await UserRepository.Add(person);
           
        }
    }
}
