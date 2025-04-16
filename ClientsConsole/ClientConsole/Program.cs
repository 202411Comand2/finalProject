using System.Text.Json;
using System.Text;

namespace ClientConsole
{
    internal class Program
    {
        static private HttpClient? client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5010")
        };
        static async Task Main()
        {
            //HttpClient? client = new HttpClient
            //{
            //    BaseAddress = new Uri("http://localhost:5010")
            //};
            // http://localhost:5010/gateway/Product/1
            // http://localhost:5010/gateway/Product/GetAll подумать над разделением
            // GET запрос
            Console.WriteLine("\nСервис Продуктов:\n");
            await ProductService();
            Console.WriteLine("\nСервис магазинов:\n");
            await ShopService();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Чтобы выйти нажмите любую клавишу");
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
            #endregion

            #region Post
            Models.Product.AddProductDto addProductDto = new();
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
            Models.Product.UpdateProductDto UpdateProductDto = new();
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
            Models.Product.DeleteProductDto DeleteProductDto = new();
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


        private static async Task ShopService()
        {

            var response = await client.GetAsync("gateway/Shop/1");
            await response.Content.ReadAsStringAsync();
            Console.WriteLine("Получить всё продукты " + await response.Content.ReadAsStringAsync() + "\n");

            #region Post
            //создание магазина
            Models.Shop.AddShopDto addShopDto = new();
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
            //
            Models.Shop.GetShopsInfoDto GetShopDto = new();
            GetShopDto.ShopIds = new List<int> { 1,2,3,4,5};
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
        }


    }
}

