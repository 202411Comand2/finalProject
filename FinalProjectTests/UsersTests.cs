using CSharpx;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using BLL.Identity.Dto;
using Newtonsoft.Json;

namespace FinalProjectTests
{
    public class UserTests
    {
        private static readonly string Controller = "https://localhost:7039/User";
        private string guest_token;
        private int randomPrefix = new Random().Next(0, 9999999);

        [Test, Order(1)]
        public async Task GetGuestToken_Test()
        {
            var client = new RestClient(Controller);
            var request = new RestRequest("/guest/login");
            var jwt = await client.GetAsync<string>(request);
            TestContext.WriteLine(jwt ?? "null");
            Assert.That(jwt, Is.Not.Null);
            guest_token = jwt;
        }
        [Test, Order(2)]
        public void RegisterUser_Test()
        {
            var client = new RestClient(Controller);
            var request = new RestRequest("/register");
            RegisterUserDto payload = new RegisterUserDto()
            {
                Username = "RegisterUserTest" + randomPrefix,
                Contact = "registerusertest"+randomPrefix+"@mail.com",
                Password = "password"
            };
            request.AddCookie("jwt", guest_token, "/", "localhost");
            request.AddBody(JsonConvert.SerializeObject(payload));
            request.Method = Method.Post;
            
            var response = client.Execute(request);
            var result = Convert.ToInt32(response.Content);
            TestContext.WriteLine($"User registered successfully with id: {result}");
            Assert.Greater(result, 0);
        }
        [Test, Order(3)]
        public void LoginUser_Test()
        {
            var client = new RestClient(Controller);
            var request = new RestRequest("/login");
            AuthDto payload = new AuthDto()
            {
                Contact = "registerusertest"+randomPrefix+"@mail.com",
                Password = "password"
            };
            request.AddCookie("jwt", guest_token, "/", "localhost");
            request.AddBody(JsonConvert.SerializeObject(payload));
            request.Method = Method.Post;
            var result = client.Execute(request);
            var newJwt = result.Cookies.Where(x => x.Name == "jwt").FirstOrDefault().Value;
            TestContext.WriteLine(newJwt ?? "null");
            Assert.That(newJwt, Is.Not.Null);
            Assert.AreNotEqual(guest_token, newJwt);
        }
    }
}