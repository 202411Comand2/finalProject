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
            var client = new RestClient("https://localhost:7039/Shop");

            // Создаем запрос
            var request = new RestRequest("add", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var newShop = new ShopDto
            {
                Name = "shops John Doe",
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(newShop);

            // Выполняем запрос
            var response = client.Execute<ShopDto>(request);

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
        public async Task Update() 
        {
            var client = new RestClient("https://localhost:7039/Shop");

            // Создаем запрос
            var request = new RestRequest("Update", Method.Put);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var updateShop = new UpdateShopDto
            {
                Id =1,
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
    }
}
