using BLL.Identity;
using DAL.Abstractions;
using Domain.Entities;
using System.Diagnostics;
using System.Text;

namespace Test
{
	public class BLLIdentyServiceTests
	{
		private IdentityService _identityService;
        private Stopwatch _stopwatch;
        private Log? _log;
        public AccessToken Token { get; set; }
        public User NewUser { get; set; }

        public BLLIdentyServiceTests(IContextManager cm, Log log)
        {
            _identityService = new IdentityService(cm);
            _stopwatch = new Stopwatch();
            _log = log;
        }

        public async Task CreateNewUserTest()
        {
            _stopwatch.Start();
            var user = await _identityService.CreateUser("Bob", "PASSWORD", "+79999076544", Token);
            _stopwatch.Stop();

            _log?.Invoke($"Created new user succesfuly: Id-{user.Id}, PW-{user.Password}, PH-{user.Phone} in {_stopwatch.ElapsedMilliseconds}ms");
            _stopwatch.Reset();
            NewUser = user;
        }

        public async Task CreateGuestTokenTest()
        {
            _stopwatch.Start();
            var token = await _identityService.GetGuestToken("Какой-то пк", "000;000;000;000");
            _stopwatch.Stop();

			_log?.Invoke($"Created new AccessToken succesfuly: Id-{token.Id}, Key-{Convert.ToBase64String(token.Key)}, Device-{token.DeviceName} CreatedAt{token.DateCreated}, Expires-{token.ExpireDate} in {_stopwatch.ElapsedMilliseconds}ms");
		    _stopwatch.Reset();
            Token = token;
        }
    }
}
