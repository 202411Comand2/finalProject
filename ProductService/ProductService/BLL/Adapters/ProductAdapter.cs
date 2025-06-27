using ProductService.Domain;

namespace ProductService.BLL
{
    /// <summary>
    /// Класс адаптер, который служит для преобразования продуктов в DTO и в Entitie
    /// </summary>
    public class ProductAdapter
    {
        /// <summary>
        /// Преобразовать из Entitie  в Dto
        /// </summary>
        /// <param name="comment">Продукт Entitie</param>
        /// <returns>ProductDto</returns>
        public static ProductDto ConvertToDTOProduct(Product product)
        {
            return new ProductDto()
            {
                ProductId = product.Id,
                ShopId = product.ShopId,
                ClusterId = product.ClusterId,
                NameProduct = product.Name,
                Description = product.Description,
                Price = product.Price,
                Barcode = product.Barcode,
                ModelNumber = product.ModelNumber,
            };
        }

        public static List<ProductDto> ConvertToDTOProduct(List<Product> productItem) 
        {
            List <ProductDto> list = new ();
            foreach (var product in productItem)
            {
                list.Add(   new ProductDto()
                {
                    ProductId = product.Id,
                    ShopId = product.ShopId,
                    ClusterId = product.ClusterId,
                    NameProduct = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Barcode = product.Barcode,
                    ModelNumber = product.ModelNumber,
                });
            }
            return list;    
        }


        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        /// <param name="comment">Продукты Dto</param>
        /// <returns>Product</returns>
        public static Product ConvertToEntity(ProductDto сommentDto)
        {
            return new Product()
            {
                Id = сommentDto.ProductId ,
                ShopId = сommentDto.ShopId ,
                ClusterId = сommentDto.ClusterId,
                Name = сommentDto.NameProduct,
                Description = сommentDto.Description,
                Price = сommentDto.Price,
                Barcode = сommentDto.Barcode,
                ModelNumber = сommentDto.ModelNumber,
            };
        }

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        /// <param name="comment">Продукты Dto</param>
        /// <returns>Product</returns>
        public static Product ConvertToEntity(AddProductDto сommentDto)
        {
            return new Product()
            {
                ShopId = сommentDto.ShopId,
                ClusterId = сommentDto.ClusterId,
                Name = сommentDto.NameProduct,
                Description = сommentDto.Description,
                Price = сommentDto.Price,
                Barcode = сommentDto.Barcode,
                ModelNumber = сommentDto.ModelNumber,
            };
        }

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        /// <param name="comment">Продукты Dto</param>
        /// <returns>Product</returns>
        public static Product ConvertToEntity(UpdateProductDto сommentDto)
        {
            return new Product()
            {
                Id = сommentDto.ProductId,
                ClusterId = сommentDto.ClusterId,
                Name = сommentDto.NameProduct,
                Description = сommentDto.Description,
                Price = сommentDto.Price,
                Barcode = сommentDto.Barcode,
                ModelNumber = сommentDto.ModelNumber,
            };
        }
        
    }
}
