using BLL.Dto.Shop;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests
{
    public class ProductTests
    {
        [Test]
        public async Task AddShop()
        {
            var client = new RestClient("https://localhost:7039");
            //var request = new RestRequest("/add");
            //var jwt = await client.GetAsync<string>(request);
            //TestContext.WriteLine(jwt ?? "null");
            //Assert.That(jwt, Is.Not.Null);


            // Создаем запрос
            var request = new RestRequest("Shop", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var newUser = new ShopDto
            {
                Name = "shops John Doe",
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(newUser);

            // Выполняем запрос
            var response = client.Execute<ShopDto>(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("shop created successfully!");
                Console.WriteLine($"shop ID: {response.Data.Id}");
            }
            else
            {
                Console.WriteLine($"shop: {response.ErrorMessage}");
            }
        }
    }
}
