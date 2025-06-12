using CartService.BLL.Dto;
using RestSharp;

namespace FinalProjectTests.Order
{
    public class CartTest
    {
        [Test]
        public async Task Add()
        {
            Random rand = new Random();

            var client = new RestClient("https://localhost:7039/Cart");
            for (int i = 0; i < 10; i++)
            {
                var request = new RestRequest("Add", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var create = new AddCartDto
                {
                    UserId = 1,
                    ProductId = rand.Next(1, 9)

                };

                request.AddJsonBody(create);

                var response = client.Execute<int>(request);

                if (response.IsSuccessful)               
                    Console.WriteLine($"Продукт добавлен в корзину. ID добавленного продукта: {response.Data}");                
                else                
                    Console.WriteLine($"Ошибка при добавлении: {response.ErrorMessage}");                
            }
        }

        [Test]
        public async Task Delete()
        {
            var client = new RestClient("https://localhost:7039/Cart");

            var request = new RestRequest("Delete", Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            var delete = new DeleteCartDto
            {
                IdCart = 1,
            };

            request.AddJsonBody(delete);

            var response = client.Execute<bool>(request);

            if (response.IsSuccessful)            
                Console.WriteLine($"Детали удаления: {response.Data}");            
            else            
                Console.WriteLine($"Ошибка при удалении: {response.ErrorMessage}");            
        }


        [Test]
        public async Task GetCartUser()
        {
            var client = new RestClient("https://localhost:7039/Cart");

            var request = new RestRequest("GetCartUser", Method.Get);

            var Dto = new GetCartDto()
            {
                IdUser = 1
            };

            request.AddQueryParameter("IdUser", Dto.IdUser);
            var response = client.Execute(request);

            if (response.IsSuccessful)            
                Console.WriteLine($"Корзина пользователя {Dto.IdUser}: {response.Content}");             
            else            
                Console.WriteLine($"Ошибка получения данных по корзине пользователя: {response.ErrorMessage}");            
        }
    }
}
