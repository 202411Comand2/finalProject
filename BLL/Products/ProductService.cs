using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using BLL.Products.Abstractions;

namespace BLL.ProductService
{
    /// <summary>
    /// Управление магазином сервис
    /// </summary>
    public class ProductService:IProductService
    {
        private readonly ProductRepository _productRepository;
       
        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="contextManager"></param>
        public ProductService(IContextManager contextManager) => _productRepository = new ProductRepository(contextManager);
       
        public async Task<bool> AddProduct(int shopId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber)
        {
            Domain.Entities.Product product = new()
            {
                Price = price,
                Name = nameProduct,
                Barcode = barcode,
                ModelNumber = modelNumber,
                Description = description,
                ClusterId = clusterId,
                ShopId = shopId,
            };
            if ((await _productRepository.Add(product)) is not null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            Domain.Entities.Product product = await _productRepository.Get(productId);
            if (product == null)
            {
                return false;   

            }
            await _productRepository.Delete(product);
            return false;
        }

        public async Task<bool> UpdateProduct(int productId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber)
        {
            Domain.Entities.Product product = await _productRepository.Get(productId);
            if (product == null) 
            { 
                return false;
            
            }
            product.Price = price;
            product.Name = nameProduct;
            product.Barcode = barcode;
            product.ModelNumber = modelNumber;
            product.Description = description;
            product.ClusterId = clusterId;
            await _productRepository.Update(product);
            return true;
        }

        public async Task<List<Domain.Entities.Product>> GetShopProducts(int shopId) => await _productRepository.GetShopProducts(shopId);

        public async Task<List<Domain.Entities.Product>> GetAllProduct()=> (List<Domain.Entities.Product>) await _productRepository.GetAll();
       
    }
}
