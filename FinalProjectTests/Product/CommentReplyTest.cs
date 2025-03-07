using BLL.Dto.Comment;
using BLL.Dto.ReplyComment;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinalProjectTests.Product
{
    public class CommentReplyTest
    {
        // POST
        [Test]
        public async Task Add()
        {
            Random rand = new Random();

            var client = new RestClient("https://localhost:7039/CommentReply");
            for (int i = 0; i < 10; i++)
            {
                // Создаем запрос
                var request = new RestRequest("Add", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                // Создаем объект, который хотим отправить

                var New = new AddReplyCommentDto()
                {
                    CommentUserId = rand.Next(0,9),
                    TextComment = $"comment {rand.Next(0,111111)}"
                };

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


        // PUT
        [Test]
        public async Task Update() 
        {
            var client = new RestClient("https://localhost:7039/CommentReply");
            // Создаем запрос
            var request = new RestRequest("Update", Method.Put);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var update = new UpdateCommentReplyDto
            {
               IdCommentReply=1,
                TextComment = "я обновить комментарий",
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
            var client = new RestClient("https://localhost:7039/CommentReply");

            // Создаем запрос
            var request = new RestRequest("Delete", Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            // Создаем объект, который хотим отправить
            var Delete = new DeleteCommentReplyDto
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
    }
}
