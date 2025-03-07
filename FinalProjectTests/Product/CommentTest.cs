using BLL.Dto.Comment;
using BLL.Dto.Favorite;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

                var New = new AddCommentDto(1, 2, rand.Next(1, 9), "", 1);

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
        }


        //DELETE
        [Test]
        public async Task Delete() 
        {
        
        }


        //GET
        public async Task GetCommentsProduct() 
        {
        
        }
    }
}
