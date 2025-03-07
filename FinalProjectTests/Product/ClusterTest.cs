using BLL.Dto.Cluster;
using BLL.Dto.Shop;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests.Product
{
    public class ClusterTest
    {
        [Test]
        public async Task Add()
        {
            var client = new RestClient("https://localhost:7039/Cluster");
            for (int i = 0; i < 10; i++)
            {
                // Создаем запрос
                var request = new RestRequest("add", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                // Создаем объект, который хотим отправить
                var newCluster = new AddClusterDto
                {
                    Name = $"Cluster root {i}",
                    NameParentCluseter = ""// корневой кластер
                };

                // Сериализуем объект в JSON и добавляем его в тело запроса
                request.AddJsonBody(newCluster);

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

            Random random = new Random();
            //делаем рaндамайзер
            for (int i = 0; i < 10; i++)
            {

                // Создаем запрос
                var request = new RestRequest("add", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                // Создаем объект, который хотим отправить
                var newCluster = new AddClusterDto
                {
                    Name = $"Cluster children {i}",
                    NameParentCluseter = $"Cluster root {random.Next(1, 9)}"// корневой кластер
                };

                // Сериализуем объект в JSON и добавляем его в тело запроса
                request.AddJsonBody(newCluster);

                // Выполняем запрос
                var response = client.Execute<int>(request);

                // Проверяем ответ
                if (response.IsSuccessful)
                {
                    Console.WriteLine("Cluster created successfully!");
                    Console.WriteLine($"Cluster ID:{response.Data}");

                }
                else
                {
                    Console.WriteLine($"Cluster: {response.ErrorMessage}");
                }
            }
        }

        [Test]
        public async Task GetAllElements()
        {
            // Создаем клиент RestSharp
            var client = new RestClient("https://localhost:7039/Cluster");

            // Создаем GET-запрос без параметров
            var request = new RestRequest("GetAllElements", Method.Get);

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
        public async Task GetChildrenElements()
        {
            // Создаем клиент RestSharp
            var client = new RestClient("https://localhost:7039/Cluster");

            // Создаем GET-запрос без параметров
            var request = new RestRequest("GetChildrenElements", Method.Get);

            var cluster = new GetClusterDto()
            {
                ClusterName = "Cluster root 4"
            };

            request.AddQueryParameter("ClusterName", cluster.ClusterName); // через get не удобно пользоваться(

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
        public async Task Update()
        {
            var client = new RestClient("https://localhost:7039/Cluster");

            // Создаем запрос
            var request = new RestRequest("Update", Method.Put);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var updateShop = new UpdateCluseterDto
            {
                Id = 1,
                NewName = "cluster update",
                newParent = ""
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(updateShop);

            // Выполняем запрос
            var response = client.Execute(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("Cluster update!");
                Console.WriteLine($"Cluster: {response.Content}");
            }
            else
            {
                Console.WriteLine($"Cluster: {response.ErrorMessage}");
            }
        }

        [Test]
        public async Task Delete()
        {
            var client = new RestClient("https://localhost:7039/Cluster");

            // Создаем запрос
            var request = new RestRequest("Delete", Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var DeleteShop = new DeleteClusterDto
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
                Console.WriteLine("Cluster Delete successfully!");
                Console.WriteLine($"Cluster Delete: {response.Data}");
            }
            else
            {
                Console.WriteLine($"Delete: {response.ErrorMessage}");
            }
        }

        [Test]
        public async Task GetRootElements()
        {
            // Создаем клиент RestSharp
            var client = new RestClient("https://localhost:7039/Cluster");

            // Создаем GET-запрос без параметров
            var request = new RestRequest("GetRootElements", Method.Get);

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
