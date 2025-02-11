using BLL.Abstractions;
using BLL.Identity;
using DAL;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Test
{
    
	public class BLLIdentyServiceTests
	{
        private readonly string _newUserName = "Bob";
        private readonly string _newUserPassword = "password";
        private readonly string _newUserPhone = "+79999999999";
        private readonly string _newUserEmail = "gobob@bobmail.com";
		private IdentityService _identityService;
        public User NewUser1 { get; set; }
        public User NewUser2 { get; set; }

        public BLLIdentyServiceTests()
        {
            var mockOptions = new Mock<IOptions<JwtOptions>>();
            var jwtOptions = new JwtOptions();
            jwtOptions.SecretKey = "ExampleKeyExampleKeyExampleKeyExampleKeyExampleKey";

			mockOptions.Setup(o => o.Value).Returns(jwtOptions);

            var jwtProvider = new JwtTokenProvider(mockOptions.Object);
            var dataHasher = new StringHasher();
            var contextManager = new ContextManager();
            _identityService = new IdentityService(contextManager, jwtProvider, dataHasher);
        }
        public async Task CreateNewUserByPhoneTest()
        {
            var user = await _identityService.RegisterByPhone(_newUserName,
                _newUserPassword,
                _newUserPhone
                );

            NewUser1 = user;
            Console.WriteLine(user.Id);
        }
        public async Task CreateNewUserWithSamePhoneTest()
        {
            User user;
            try
            {
                user = await _identityService.RegisterByPhone(
                    _newUserName,
                    _newUserPassword,
                    _newUserPhone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Если сейчас вылезла ошибка о том что нарушено уникальное значение" +
                    " - всё работает правильно");
            }
        }
        public async Task CreateNewUserByEmailTest()
        {
            var user = await _identityService.RegisterByEmail(
                _newUserName,
                _newUserPassword,
                _newUserEmail);

            NewUser2 = user;
            Console.WriteLine(user.Id);
        }
		public async Task CreateNewUserWithSameEmailTest()
		{
			User user;
			try
			{
				user = await _identityService.RegisterByEmail(
					_newUserName,
					_newUserPassword,
					_newUserEmail);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Me);
				Console.WriteLine("Если сейчас вылезла ошибка о том что нарушено уникальное значение" +
					" - всё работает правильно");
			}
		}
		public async Task CreateGuestTokenNotNullTest()
        {
            var token = await _identityService.GetGuestToken();

            Console.WriteLine(token);
        }
        public async Task LogInByEmailTest()
        {
            var result = await _identityService.AuthUserByEmail(_newUserEmail, _newUserPassword);

            Console.WriteLine(result);
        }
        public async Task LogInByPhoneTest()
        {
            var result = await _identityService.AuthUserByPhone(_newUserPhone, _newUserPassword);

            Console.WriteLine(result);
        }
    }
}
