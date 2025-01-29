using Domain.Entities;
using DAL.Repositories;
using DAL;

namespace Test
{
    public delegate void Log(string message);
    internal class Program
    {
        private static ContextManager _contextManager;
        private static BLLIdentyServiceTests _bllIdentityTests;

        static async Task Main(string[] args)
        {
            _contextManager = new ContextManager();
            _bllIdentityTests = new BLLIdentyServiceTests(_contextManager, Log);
            await _bllIdentityTests.CreateGuestTokenTest();
            await _bllIdentityTests.CreateNewUserTest();
        }
        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        //static async public Task AddNewUser()
        //{
        //        Cart cart = new Cart()
        //        {
        //        };

        //        AccessToken authToken = new AccessToken()
        //        {
        //            //TokenId = new Guid(),
        //            DeviceName = "phone",
        //            ExpireDate = DateTime.UtcNow.AddDays(1),
        //        };

        //        Random random = new Random();
        //        User person = new User()
        //        {
        //            Name = $"Oleg{random.Next(0, 1000)}",
        //            //Password = "1234Pasword",
        //            Phone = "71234567899",
        //            Email = "testEmail@Yndex.ru",
        //            TelegramId = 0,
        //            //AccessToken = authToken,

        //        };
        //        //await UserRepository.Add(person);
           
        //}
    }
}
