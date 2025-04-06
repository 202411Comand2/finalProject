using Microsoft.AspNetCore.Mvc;
namespace ShopAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<string> _products = new() { "Laptop", "Phone", "Tablet" };


        [HttpGet("TestingCore")]
        public IActionResult GetProducts()
        {
            return Ok(new[] { "Product1", "Product2" });
        }

        /// <summary>
        /// Получить все продукты
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products);
        }

        /// <summary>
        /// Добавить новый продукт
        /// </summary>
        /// <param name="productName">Название продукта</param>
        [HttpPost]
        public IActionResult Add(string productName)
        {
            _products.Add(productName);
            return Ok($"Продукт '{productName}' добавлен");
        }

        /// <summary>
        /// Получить продукт по ID
        /// </summary>
        /// <param name="id">ID продукта (0-2)</param>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id < 0 || id >= _products.Count)
                return NotFound("Продукт не найден");

            return Ok(_products[id]);
        }
    }
}
