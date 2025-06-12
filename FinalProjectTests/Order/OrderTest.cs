using OrderService.BLL.Dto;
using RestSharp;

namespace FinalProjectTests.Order
{
    public class OrderTest
    {
        [Test]
        public async Task Add()
        {
            Random rand = new Random();

            var client = new RestClient("https://localhost:7039/Order");
            for (int i = 0; i < 10; i++)
            {
                var request = new RestRequest("Add", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var create = new AddOrderDto
                {
                    UserId = 1,
                    ProductId = rand.Next(1, 9)

                };

                request.AddJsonBody(create);

                var response = client.Execute<int>(request);

                if (response.IsSuccessful)
                    Console.WriteLine($"Создан заказ. ID добавленного заказа: {response.Data}");
                else
                    Console.WriteLine($"Ошибка при добавлении: {response.ErrorMessage}");
            }
        }

        [Test]
        public async Task Delete()
        {
            var client = new RestClient("https://localhost:7039/Order");

            var request = new RestRequest("Delete", Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            var delete = new DeleteOrderDto
            {
                IdOrder = 1,
            };

            request.AddJsonBody(delete);

            var response = client.Execute<bool>(request);

            if (response.IsSuccessful)
                Console.WriteLine($"Детали удаления заказа: {response.Data}");
            else
                Console.WriteLine($"Ошибка при удалении заказа: {response.ErrorMessage}");
        }


        [Test]
        public async Task GetOrderUser()
        {
            var client = new RestClient("https://localhost:7039/Order");

            var request = new RestRequest("GetOrderUser", Method.Get);

            var Dto = new GetOrderDto()
            {
                IdUser = 1
            };

            request.AddQueryParameter("IdUser", Dto.IdUser);
            var response = client.Execute(request);

            if (response.IsSuccessful)
                Console.WriteLine($"Заказы пользователя {Dto.IdUser}: {response.Content}");
            else
                Console.WriteLine($"Ошибка получения данных по заказам пользователя: {response.ErrorMessage}");
        }
    }
}

