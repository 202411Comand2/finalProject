using RestSharp;

namespace FinalProjectTests
{
    public class UserTests
    {
        [Test]
        public async Task GetToken_Test()
        {
            var client = new RestClient("https://localhost:7039/User");
            var request = new RestRequest("/guestLogin");
            var jwt = await client.GetAsync<string>(request);
            TestContext.WriteLine(jwt ?? "null");
            Assert.That(jwt, Is.Not.Null);
        }
    }
}