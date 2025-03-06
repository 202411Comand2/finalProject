using BLL.Dto.Shop;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests
{
    public class ShopsTests
    {
        [Test]
        public async Task AddShop()
        {
            var client = new RestClient("https://localhost:7039/Shop");
            for (int i = 0; i < 10; i++)
            {
                // Создаем запрос
                var request = new RestRequest("add", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                // Создаем объект, который хотим отправить
                var newShop = new ShopDto
                {
                    Name = $"shops John Doe {i}",
                };

                // Сериализуем объект в JSON и добавляем его в тело запроса
                request.AddJsonBody(newShop);

                // Выполняем запрос
                var response = client.Execute<int>(request);
              //  Assert.True(response.IsSuccessful);
                // Проверяем ответ
                if (response.IsSuccessful)
                {
                    Console.WriteLine("shop created successfully!");
                    Console.WriteLine($"shop ID: {response.Data}");
                }
                else
                {
                    Console.WriteLine($"shop: {response.ErrorMessage}");
                }
            }
        }

        [Test]
        public async Task Update()
        {
            var client = new RestClient("https://localhost:7039/Shop");

            // Создаем запрос
            var request = new RestRequest("Update", Method.Put);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var updateShop = new UpdateShopDto
            {
                Id = 1,
                NewName = "shops John Doe Я обновить магазин",
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(updateShop);

            // Выполняем запрос
            var response = client.Execute<UpdateShopDto>(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("shop created successfully!");
                Console.WriteLine($"shop ID: {response.Data}");
            }
            else
            {
                Console.WriteLine($"shop: {response.ErrorMessage}");
            }
        }

        [Test]
        public async Task Delete()
        {
            var client = new RestClient("https://localhost:7039/Shop");

            // Создаем запрос
            var request = new RestRequest("Delete", Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var DeleteShop = new DeleteShopDto
            {
                Id = 1,
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(DeleteShop);

            // Выполняем запрос
            var response = client.Execute<bool>(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("shop Delete successfully!");
                Console.WriteLine($"shop Delete: {response.Data}");
            }
            else
            {
                Console.WriteLine($"Delete: {response.ErrorMessage}");
            }
        }

        [Test]
        public async Task GetInfo()
        {
            var client = new RestClient("https://localhost:7039/Shop");

            // Создаем запрос
            var request = new RestRequest("GetInfo", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            // Создаем объект, который хотим отправить

            List<int> intShopId = new List<int>() { 1,2,3,4,5,6,7,8,9};

            var newShop = new GetShopsInfoDto
            {
                ShopIds = intShopId
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(newShop);

            // Выполняем запрос
            //var response = client.Execute<List<ShopDto>>(request);
            var response = client.Execute(request);
            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("shop created successfully!");
                Console.WriteLine("Response content: " + response.Content);

            }
            else
            {
                Console.WriteLine($"shop: {response.ErrorMessage}");
            }
        }
    }
}
