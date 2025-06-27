using FavoriteService.BLL;
using RestSharp;

namespace FinalProjectTests.Product
{
    public class FavoriteTest
    {
        [Test]
        public async Task add()
        {
            Random rand = new Random();

            var client = new RestClient("https://localhost:7039/Favorite");
            for (int i = 0; i < 10; i++)
            {
                // Создаем запрос
                var request = new RestRequest("Add", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                // Создаем объект, который хотим отправить

                var NewFavorite = new AddFavoriteDto
                {
                    UserId = 1,
                    ProductId = rand.Next(1, 9)

                };

                // Сериализуем объект в JSON и добавляем его в тело запроса
                request.AddJsonBody(NewFavorite);

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
        public async Task Delete()
        {
            var client = new RestClient("https://localhost:7039/Favorite");

            // Создаем запрос
            var request = new RestRequest("Delete", Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var DeleteShop = new DeleteFavoriteDto
            {
                IdFavorite = 11,
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
        public async Task GetFavoriteUser()
        {
            // Создаем клиент RestSharp
            var client = new RestClient("https://localhost:7039/Favorite");

            // Создаем GET-запрос без параметров
            var request = new RestRequest("GetFavoriteUser", Method.Get);


            var Dto = new GetFavoriteDto()
            {
                IdUser = 1
            };
            // Выполняем запрос
            request.AddQueryParameter("IdUser", Dto.IdUser); // через get не удобно пользоваться(
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
