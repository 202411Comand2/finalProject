using Client.Models;
using System.Text;
using System.Text.Json;

namespace ClientConsole
{
    internal class Program
    {
        static private HttpClient? client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:5011")
        };
        static async Task Main()
        {
           
            Console.WriteLine("\nСервис Продуктов:\n");
            await ProductService();
            //Console.WriteLine("\nСервис магазинов:\n");
            //await ShopService();
            //Console.WriteLine("\nСервис избранного:\n");
            //await FavoriteService();
            //Console.WriteLine("\nСервис комментов:\n");
            //await CommentService();

            //Console.WriteLine("\nСервис кластеров:\n");
            //await ClusterService();


            //Console.ForegroundColor = ConsoleColor.Red;
            
            //Console.WriteLine("Чтобы выйти нажмите любую клавишу");
           Console.ReadKey();
             return;

        }

        /// <summary>
        /// Написание запроса для productService
        /// </summary>
        /// <returns></returns>
        private static async Task ProductService()
        {
            #region get запросы
            var response = await client.GetAsync("gateway/Product/GetAll");
            await response.Content.ReadAsStringAsync();
            Console.WriteLine("Получить всё продукты " + await response.Content.ReadAsStringAsync() + "\n");

            string idShop = "1";
            response = await client.GetAsync($"gateway/Product/GetShopProducts?ShopId={idShop}");
            await response.Content.ReadAsStringAsync();
            Console.WriteLine("Получить по id магазину " + await response.Content.ReadAsStringAsync() + "\n");

            string cluster = "0";
            var response1 = await client.GetAsync($"gateway/Product/GetProductsByCluster?ClusterId={cluster}");
            await response1.Content.ReadAsStringAsync();
            Console.WriteLine("Получить по id кластеру " + await response1.Content.ReadAsStringAsync() + "\n");


            string id = "1";
            response = await client.GetAsync($"gateway/Product/GetProductsByCluster?id={id}");
            await response.Content.ReadAsStringAsync();
            Console.WriteLine("Получить по id магазину " + await response.Content.ReadAsStringAsync() + "\n");
            return;
            #endregion

            #region Post 
            // Добаление продукта
            AddProductDto addProductDto = new();
            addProductDto.ShopId = 0;
            addProductDto.ClusterId = 0;
            addProductDto.NameProduct = "post ";
            addProductDto.Description = "description";
            addProductDto.Price = 0;
            addProductDto.Barcode = 0;
            addProductDto.ModelNumber = "modelNumber";
            // 2. Подготавливаем данные (объект → JSON)
            string json = JsonSerializer.Serialize(addProductDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // 3. Отправляем POST-запрос
            response = await client.PostAsync("gateway/Product/add", content);


            int idResponseObject = -1; //запысываем id объекта чтобы его изменить

            // 4. Проверяем ответ
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ответ сервера id созданного объекта: {responseBody}");
                int.TryParse(responseBody, out idResponseObject);
            }
            else
            {
                Console.WriteLine($"Ошибка: {response.StatusCode}");
            }
            #endregion

            #region put 
            //Обновить информацию по продукту
            UpdateProductDto UpdateProductDto = new();
            UpdateProductDto.ProductId = idResponseObject;
            UpdateProductDto.ClusterId = 0;
            UpdateProductDto.NameProduct = "put update";
            UpdateProductDto.Description = "description";
            UpdateProductDto.Price = 0;
            UpdateProductDto.Barcode = 0;
            UpdateProductDto.ModelNumber = "modelNumber";
            // 2. Подготавливаем данные (объект → JSON)
            string jsonPut = JsonSerializer.Serialize(UpdateProductDto);
            StringContent contentPut = new StringContent(jsonPut, Encoding.UTF8, "application/json");
            // 3. Отправляем POST-запрос
            response = await client.PutAsync("gateway/Product/Update", contentPut);

            // 4. Проверяем ответ
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ответ сервера получилось ли изменить объект: {responseBody}");
            }
            else
            {
                Console.WriteLine($"Ошибка: {response.StatusCode}");
            }
            #endregion

            #region delete

            // 1. Создаем HttpClient
            using var httpClient = new HttpClient();

            // 2. Устанавливаем базовый адрес (если нужно)
            httpClient.BaseAddress = new Uri("http://localhost:5010");

            // 3. Добавляем заголовки
            httpClient.DefaultRequestHeaders.Add("accept", "text/plain");

            // 4. Создаем DTO для удаления
            DeleteProductDto DeleteProductDto = new();
            DeleteProductDto.Id = idResponseObject;


            string jsonDelete = JsonSerializer.Serialize(DeleteProductDto);
            StringContent contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");

            // 5. Создаем DELETE-запрос с телом
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("gateway/Product/Delete", UriKind.Relative),
                Content = contentDelete
            };

            // 6. Отправляем запрос
            try
            {
                var responseDel = await httpClient.SendAsync(request);

                if (responseDel.IsSuccessStatusCode)
                {
                    string responseBody = await responseDel.Content.ReadAsStringAsync();
                    Console.WriteLine($"Успешно удалено. Ответ: {responseBody}");
                }
                else
                {
                    Console.WriteLine($"Ошибка: {responseDel.StatusCode}");
                    string errorContent = await responseDel.Content.ReadAsStringAsync();
                    Console.WriteLine($"Детали: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
            }

            #endregion
        }

        /// <summary>
        /// Сервис магазина
        /// </summary>
        /// <returns></returns>
        private static async Task ShopService()
        {

            HttpResponseMessage response = await client.GetAsync("gateway/Shop/1");
            await response.Content.ReadAsStringAsync();
            Console.WriteLine("Получить всё продукты " + await response.Content.ReadAsStringAsync() + "\n");

            #region Post
            #region создание магазина
            AddShopDto addShopDto = new();
            addShopDto.Name = "modelNumbersdadasdasdas";
            // 2. Подготавливаем данные (объект → JSON)
            string json = JsonSerializer.Serialize(addShopDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // 3. Отправляем POST-запрос
            response = await client.PostAsync("gateway/Shop/add", content);


            int idResponseObject = -1; //запысываем id объекта чтобы его изменить

            // 4. Проверяем ответ
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ответ сервера id созданного объекта: {responseBody}");
                int.TryParse(responseBody, out idResponseObject);
            }
            else
            {
                Console.WriteLine($"Ошибка: {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            }
            #endregion
           
            #region получить информацию о магазинах по list
            GetShopsInfoDto GetShopDto = new();
            GetShopDto.ShopIds = new List<int> { 1, 2, 3, 4, 5 };
            // 2. Подготавливаем данные (объект → JSON)
            json = JsonSerializer.Serialize(GetShopDto);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            // 3. Отправляем POST-запрос
            response = await client.PostAsync("gateway/Shop/GetInfo", content);


            idResponseObject = -1; //запысываем id объекта чтобы его изменить

            // 4. Проверяем ответ
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Список магазинов по массиву: {responseBody}");
                int.TryParse(responseBody, out idResponseObject);
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка: {response.StatusCode} {responseBody}");
            }
            #endregion

            #endregion
            
            #region put

            #region Обновить название магазина
            UpdateShopDto UpdateShopDto = new();
            UpdateShopDto.Id = 1;
            UpdateShopDto.NewName = "Сам лучший магазин";
            // 2. Подготавливаем данные (объект → JSON)
            response = await client.PutAsync("gateway/Shop/Update", CreateJson(UpdateShopDto));
            // 4. Проверяем ответ
            await CheckedAnswer(response);
            #endregion


            #region Восстановить магазин
            RestoreShopDto RestoreShopDto = new();
            RestoreShopDto.Id = 1;
            // 2. Подготавливаем данные (объект → JSON)
            response = await client.PutAsync("gateway/Shop/RestoreShop", CreateJson(UpdateShopDto));
            // 4. Проверяем ответ
            await CheckedAnswer(response);
            #endregion

            #endregion

            #region Delete удаление магазина


            // 4. Создаем DTO для удаления
            DeleteShopDto deleteShopDto = new();
            deleteShopDto.Id = 1;

            // 5. Создаем DELETE-запрос с телом
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("gateway/Shop/Delete", UriKind.Relative),
                Content = CreateJson(deleteShopDto) 
            };

            // 6. Отправляем запрос

            response = await client.SendAsync(request);
            await  CheckedAnswer(response);

            #endregion
        }

        /// <summary>
        /// Избранные позиции
        /// </summary>
        /// <returns></returns>
        private static async Task FavoriteService()
        {
            #region Post
            #region добавление избранной позиции
            AddFavoriteDto addFavoriteDto = new();
            addFavoriteDto.UserId = 12;
            addFavoriteDto.ProductId = 12;
            HttpResponseMessage response = await client.PostAsync("gateway/Favorite/Add", CreateJson(addFavoriteDto));
            // 4. Проверяем ответ
            await CheckedAnswer(response);
            #endregion
            #endregion

            #region Delete избранной позиции


            // 4. Создаем DTO для удаления
            DeleteFavoriteDto deleteFavoriteDto = new();
            deleteFavoriteDto.IdFavorite = 1;

            // 5. Создаем DELETE-запрос с телом
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("gateway/Favorite/Delete", UriKind.Relative),
                Content = CreateJson(deleteFavoriteDto)
            };

            // 6. Отправляем запрос
            response = await client.SendAsync(request);
            await CheckedAnswer(response);
            #endregion

            #region get запросы
            string intUserId = "1";
            response = await client.GetAsync($"gateway/Favorite/GetFavoriteUser?IdUser={intUserId}");
            await response.Content.ReadAsStringAsync();
            Console.WriteLine("Получить всё избранные позиции " + await response.Content.ReadAsStringAsync() + "\n");

           
            #endregion


        }

        /// <summary>
        /// Работа с комментариями
        /// </summary>
        /// <returns></returns>
        private static async Task CommentService() 
        {
            #region Post
            // добавление комментария
            AddCommentDto addDto = new(1,"name",1,1,"commet text",12);
            await PostAsync(addDto, "gateway/Comment/Add");
            #endregion

            #region Post
            // добавление комментария
            AddCommentDto addDto1 = new(2, "name", 1, 1, "commet text", 12);
            await PostAsync(addDto, "gateway/Comment/Add");
            #endregion

            #region delete удаление магазина
            //удаление комменатрия
            DeleteCommentDto deleteDto = new(1);
            await DeleteAsync(deleteDto, "gateway/Comment/Delete");
            #endregion

            #region put
            // обновление комментария
            UpdateCommentDto updateDto = new(1, "Новый комментарий",4);
            await PutAsync(updateDto, "gateway/Comment/Update");
            #endregion

            #region get получить всё комментарии по магазину
            GetAllCommentsProduct getDto = new(1);
            await GetAsync(getDto, $"gateway/Comment/GetCommentsProduct?IdProduct={getDto.IdProduct}");
            #endregion

            Console.WriteLine("-----------------Ответные комменатрия----------------");

            //ответные комментария CommentReply
            #region Post
            // добавление комментария
            AddReplyCommentDto addRepDto = new(2,"Сам такой");
            await PostAsync(addRepDto, "gateway/CommentReply/Add");
            #endregion

            #region delete удаление комментария
            //удаление комменатрия
            DeleteCommentReplyDto deletdRepDto = new(1);
            await DeleteAsync(deletdRepDto, "gateway/CommentReply/Delete");
            #endregion

            #region put
            // обновление комментария
            UpdateCommentReplyDto updateRepDto = new(1, "Новый комментарий исправленый");
            await PutAsync(updateRepDto, "gateway/CommentReply/Update");
            #endregion



        }


        private static async Task ClusterService() 
        {
            #region Post
            // добавление кластера
            AddClusterDto addDto = new("Как-то имя кластера","");
            await PostAsync(addDto, "gateway/Cluster/Add");
            #endregion

            #region put
            // обновление кластера
            UpdateCluseterDto updateDto = new(1,"Обновлённое название кластера","");
            await PutAsync(updateDto, "gateway/Cluster/Update");
            #endregion

            #region delete удаление магазина
            //удаление кластера
            DeleteClusterDto deleteDto = new(1);
            await DeleteAsync(deleteDto, "gateway/Cluster/Delete");
            #endregion

            #region get получить всё комментарии по магазину
            await GetAsync("",$"gateway/Cluster/GetAllElements");

            await GetAsync("", $"gateway/Cluster/GetRootElements");

            GetClusterDto getClusterDto = new("Обновлённое название кластера");
            await GetAsync("", $"gateway/Cluster/GetChildrenElements?ClusterName={getClusterDto.ClusterName}");
            #endregion
            
            Console.WriteLine("---------------SearchCluster-----------------");

        }



        /// <summary>
        /// Создать StringContent из объекта для подготовки данных (объект → JSON)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects"></param>
        /// <returns></returns>
        private static StringContent CreateJson<T> (T objects) => 
            new StringContent(JsonSerializer.Serialize(objects), Encoding.UTF8, "application/json");
  
        /// <summary>
        /// Проверить ответ от сервера
        /// </summary>
        /// <param name="response">Ответ сервера</param>
        /// <param name="printMessage">Печатать ли в консоли ответ</param>
        /// <returns></returns>
        private async static Task<string> CheckedAnswer(HttpResponseMessage response, bool printMessage = true) 
        {
            string responseBody = "";
            bool error = false;
            if (response.IsSuccessStatusCode)
            {
                error = false;
                responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                error = true;
                responseBody = await response.Content.ReadAsStringAsync();
            }
            if (printMessage) 
            {
                if (error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка:\n" + responseBody);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ответ от сервера:\n" + responseBody);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            return responseBody;
        }

        #region запросы post, get, delete, update
   
        /// <summary>
        /// Пост запрос для простоты вынес в отдельный метод
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectDto">модель DTO</param>
        /// <param name="puthResponse">Запрос</param>
        /// <returns></returns>
        private async static Task PostAsync<T>(T objectDto, string puthResponse) 
        {
            HttpResponseMessage response = await client.PostAsync(puthResponse, CreateJson(objectDto));
            // 4. Проверяем ответ
            await CheckedAnswer(response);
        }

        /// <summary>
        /// Put запрос для простоты вынес в отдельный метод
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectDto">модель DTO</param>
        /// <param name="puthResponse">Запрос</param>
        /// <returns></returns>
        private async static Task PutAsync<T>(T objectDto, string puthResponse)
        {
            // 2. Подготавливаем данные (объект → JSON)
            HttpResponseMessage response = await client.PutAsync(puthResponse, CreateJson(objectDto));
            // 4. Проверяем ответ
            await CheckedAnswer(response);
        }

        /// <summary>
        /// delete запрос для простоты вынес в отдельный метод
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectDto">модель DTO</param>
        /// <param name="puthResponse">Запрос</param>
        /// <returns></returns>
        private async static Task DeleteAsync<T>(T objectDto, string puthResponse)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(puthResponse, UriKind.Relative),
                Content = CreateJson(objectDto)
            };
            HttpResponseMessage response = await client.SendAsync(request);
            await CheckedAnswer(response);
        }

        /// <summary>
        /// Get запрос для простоты вынес в отдельный метод
        /// </summary>
        /// <param name="puthResponse">Запрос</param>
        /// <returns></returns>
        private async static Task GetAsync<T>(T objectDto, string puthResponse)
        {
            var response = await client.GetAsync(puthResponse);
            await response.Content.ReadAsStringAsync();
            Console.WriteLine("Получить всё объекты из get запроса:\n" + await response.Content.ReadAsStringAsync() + "\n");
        }




        #endregion
    }
}

