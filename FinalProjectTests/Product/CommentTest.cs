using CommentService.BLL;
using RestSharp;

namespace FinalProjectTests.Product
{
    public class CommentTest
    {
        //  POST
        [Test]
        public async Task Add()
        {
            Random rand = new Random();

            var client = new RestClient("https://localhost:7039/Comment");
            for (int i = 0; i < 10; i++)
            {
                // Создаем запрос
                var request = new RestRequest("Add", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                // Создаем объект, который хотим отправить

                var New = new AddCommentDto(1, "User", 2, rand.Next(1, 9), "", 1);

                // Сериализуем объект в JSON и добавляем его в тело запроса
                request.AddJsonBody(New);

                // Выполняем запрос
                var response = client.Execute<int>(request);

                // Проверяем ответ
                if (response.IsSuccessful)
                {
                    Console.WriteLine($"Comment created successfully! {response.Content}");
                }
                else
                {
                    Console.WriteLine($"Comment not create: {response.ErrorMessage}");
                }
            }

        }
        //PUT
        [Test]
        public async Task Update() 
        {
            var client = new RestClient("https://localhost:7039/Comment");
            // Создаем запрос
            var request = new RestRequest("Update", Method.Put);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var update = new UpdateCommentDto
            {
                CommentId = 2,
                TextComment = "я обновить комментарий",
                Estimation = 123
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(update);

            // Выполняем запрос
            var response = client.Execute<bool>(request);

            // Проверяем ответ
            if (response.IsSuccessful)
            {
                Console.WriteLine("shop update successfully!");
                Console.WriteLine($"shop update: {response.Data}");
            }
            else
            {
                Console.WriteLine($"shop: {response.ErrorMessage}");
            }
        }


        //DELETE
        [Test]
        public async Task Delete() 
        {
            var client = new RestClient("https://localhost:7039/Comment");

            // Создаем запрос
            var request = new RestRequest("Delete", Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var Delete = new DeleteCommentDto
            {
                Id = 1,
            };

            // Сериализуем объект в JSON и добавляем его в тело запроса
            request.AddJsonBody(Delete);

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


        //GET
        [Test]
        public async Task GetCommentsProduct() 
        {
            // Создаем клиент RestSharp
            var client = new RestClient("https://localhost:7039/Comment");

            // Создаем GET-запрос без параметров
            var request = new RestRequest("GetCommentsProduct", Method.Get);

            var productsDto = new GetAllCommentsProduct()
            {
                IdProduct = 1
            };

            request.AddQueryParameter("IdProduct", productsDto.IdProduct); // через get не удобно пользоваться(

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
