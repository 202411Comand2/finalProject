using BLL.Dto.Cluster;
using BLL.Dto.Product;
using BLL.Dto.Shop;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests
{
    public class ProductsTest
    {
        [Test]
        public async Task add()
        {
            Random rand = new Random();

            var client = new RestClient("https://localhost:7039/Product");
            for (int i = 0; i < 10; i++)
            {
                // Создаем запрос
                var request = new RestRequest("add", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                // Создаем объект, который хотим отправить

                var newProduct = new AddProductDto
                {
                    ShopId = 2,
                    ClusterId = 2,
                    NameProduct = $"Product {i}",
                    Description = $"description {i}",
                    Price = rand.Next(1, 10000000),
                    Barcode = 1234,
                    ModelNumber = $"123 {i} {rand.Next(1, 222)}"
                };

                // Сериализуем объект в JSON и добавляем его в тело запроса
                request.AddJsonBody(newProduct);

                // Выполняем запрос
                var response = client.Execute<int>(request);

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
            var client = new RestClient("https://localhost:7039/Product");

            // Создаем запрос
            var request = new RestRequest("Update", Method.Put);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var updateProduct = new UpdateProductDto
            {
                ProductId = 2,
                ClusterId = 4,
                NameProduct = "Update product",
                Description = "Update string",
                Price = -110,
                Barcode = 11,
                ModelNumber = "222222"
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(updateProduct);

            // Выполняем запрос
            var response = client.Execute<bool>(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("Product created successfully!");
                Console.WriteLine($"Product result: {response.Data}");
            }
            else
            {
                Console.WriteLine($"result: {response.ErrorMessage}");
            }
        }

        [Test]  
        public async Task Delete() 
        {
            var client = new RestClient("https://localhost:7039/Product");

            // Создаем запрос
            var request = new RestRequest("Delete", Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var DeleteProduct = new DeleteProductDto
            {
                Id = 8,
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(DeleteProduct);

            // Выполняем запрос
            var response = client.Execute<bool>(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("Product Delete successfully!");
                Console.WriteLine($"Product Delete: {response.Data}");
            }
            else
            {
                Console.WriteLine($"Product don't delete: {response.ErrorMessage}");
            }
        }

        [Test]
        public async Task GetShopProducts() 
        {
            // Создаем клиент RestSharp
            var client = new RestClient("https://localhost:7039/Product");

            // Создаем GET-запрос без параметров
            var request = new RestRequest("GetChildrenElements", Method.Get);

            var productsDto = new GetallShopProductsDto()
            {
                ShopId = 2
            };

            request.AddQueryParameter("ShopId", productsDto.ShopId); // через get не удобно пользоваться(

            // Выполняем запрос
            var response = client.Execute(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("GET request successful!");
                Console.WriteLine("Response content: " + response.Content);
            }
            else
            {
                Console.WriteLine($"Error: {response.ErrorMessage}");
            }
        }

        [Test]
        public async Task GetAll()
        {
            // Создаем клиент RestSharp
            var client = new RestClient("https://localhost:7039/Product");

            // Создаем GET-запрос без параметров
            var request = new RestRequest("GetAll", Method.Get);

            // Выполняем запрос
            var response = client.Execute(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("GET request successful!");
                Console.WriteLine("Response content: " + response.Content);
            }
            else
            {
                Console.WriteLine($"Error: {response.ErrorMessage}");
            }
        }

        [Test]
        public async Task GetProductsByCluster() 
        {
            // Создаем клиент RestSharp
            var client = new RestClient("https://localhost:7039/Product");

            // Создаем GET-запрос без параметров
            var request = new RestRequest("GetProductsByCluster", Method.Get);

            var productsDto = new GetAllClusterProductsDto()
            {
                ClusterId = 2
            };

            request.AddQueryParameter("ClusterId", productsDto.ClusterId); // через get не удобно пользоваться(

            // Выполняем запрос
            var response = client.Execute(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("GET request successful!");
                Console.WriteLine("Response content: " + response.Content);
            }
            else
            {
                Console.WriteLine($"Error: {response.ErrorMessage}");
            }
        }
    }
}
