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
using BLL.Dto.Product;
using BLL.Dto;

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
       
        public async Task<AnswerWithBackendDto<ProductDto>> AddProduct(AddProductDto productDto)//int shopId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber)
        {
            AnswerWithBackendDto<ProductDto> answerWithBackendDto = new();
           // Domain.Entities.Product product = Adapters.ProductAdapter.ConvertToEntity(productDto);
            var addProduct = await _productRepository.Add(Adapters.ProductAdapter.ConvertToEntity(productDto));
            if (addProduct is null)
            {
                answerWithBackendDto.AddErrorLog("Не получилось добавить товар ");
                return answerWithBackendDto;
            }
            else 
            {
                answerWithBackendDto.AddObject(Adapters.ProductAdapter.ConvertToDTOProduct(addProduct));
                return answerWithBackendDto;
            }
        }

        public async Task<AnswerWithBackendDto<ProductDto>> DeleteProduct(DeleteProductDto productDto)
        {
            AnswerWithBackendDto<ProductDto> answerWithBackendDto = new();
            Domain.Entities.Product product = await _productRepository.Get(productDto.Id);
            if (product == null)
            {
                answerWithBackendDto.AddErrorLog($"Не удалось удалить продукт по указанному id = {productDto.Id}");
                return answerWithBackendDto;   

            }
            product.IsDeleted = true;
            answerWithBackendDto.AddObject(Adapters.ProductAdapter.ConvertToDTOProduct( await _productRepository.Update(product)));
            return answerWithBackendDto;
        }

        public async Task<AnswerWithBackendDto<ProductDto>> UpdateProduct(UpdateProductDto productDto) //,int ProductId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber)
        {
            AnswerWithBackendDto<ProductDto> answerWithBackendDto = new();
            Domain.Entities.Product product = Adapters.ProductAdapter.ConvertToEntity(productDto);
            Domain.Entities.Product _ = await _productRepository.Get(product.Id);
            if (_ is null) 
            {
                answerWithBackendDto.AddErrorLog($"Не найден объект по указанному id = {productDto.ProductId}");
                return answerWithBackendDto;
            }
            product.ShopId = _.ShopId;
            answerWithBackendDto.AddObject(Adapters.ProductAdapter.ConvertToDTOProduct(await _productRepository.Update(product)));
            return answerWithBackendDto;
        }

        public async Task<AnswerWithBackendDto<ProductDto>> GetShopProducts(GetallShopProductsDto productDto) 
        {
            AnswerWithBackendDto<ProductDto> answerWithBackendDto = new();
            var items = await _productRepository.GetShopProducts(productDto.ShopId);
            if (items == null) 
            {
                answerWithBackendDto.AddErrorLog("Произошла ошибка при обращении к бд.");
            }
            if (items?.Count == 0)
            {
                answerWithBackendDto.AddErrorLog($"Данные отсутствуют.");
            }
            answerWithBackendDto.AddObject(Adapters.ProductAdapter.ConvertToDTOProduct(items));
            return answerWithBackendDto;
        }

        public async Task<AnswerWithBackendDto<ProductDto>> GetAllProduct() 
        {
            AnswerWithBackendDto<ProductDto> answerWithBackendDto = new();
            List<Product> items = (List<Product>)  await _productRepository.GetAll();
            if (items == null)
            {
                answerWithBackendDto.AddErrorLog("Произошла ошибка при обращении к бд.");
            }
            if (items?.Count == 0)
            {
                answerWithBackendDto.AddErrorLog($"Данные отсутствуют.");
            }
            answerWithBackendDto.AddObject(Adapters.ProductAdapter.ConvertToDTOProduct(items));
            return answerWithBackendDto;
        }

        public async Task<AnswerWithBackendDto<ProductDto>> GetProductsByCluster(GetAllClusterProductsDto productDto) 
        {
            AnswerWithBackendDto<ProductDto> answerWithBackendDto = new();
            var items = await _productRepository.GetProductsByCluster(productDto.ClusterId);
            if (items == null)
            {
                answerWithBackendDto.AddErrorLog("Произошла ошибка при обращении к бд.");
            }
            if (items?.Count == 0)
            {
                answerWithBackendDto.AddErrorLog($"Данные отсутствуют.");
            }
            answerWithBackendDto.AddObject(Adapters.ProductAdapter.ConvertToDTOProduct(items));
            return answerWithBackendDto;
        } 

        public async Task<bool> AddReting(ProductDto productDto, decimal reting)
        {
            // int ProductId, decimal reting

            Domain.Entities.Product product = await _productRepository.Get(Adapters.ProductAdapter.ConvertToEntity(productDto).Id);
            if (product == null)
            {
                return false;
            }
            if (product.AmountOfComments == 0)
            {
                product.AverageRating = reting;
                product.AmountOfComments = 1;
                await _productRepository.Update(product);
                return true;
            }
            else 
            {
                product.AmountOfComments = product.AmountOfComments + 1;
                product.AverageRating = (product.AverageRating * product.AmountOfComments + reting) / product.AmountOfComments;
                // востанавливаем рейтинг и прибавляем новые данные, потом делем на количество отзывом
                await _productRepository.Update(product);
                return true; 
            }
        }

        public async Task<bool> UpdateReting(int productId, decimal newReting, decimal oldReting)
        {
            Domain.Entities.Product product = await _productRepository.Get(productId);
            decimal reting = newReting - oldReting;
            if (product == null)
            {
                return false;
            }
            if (product.AmountOfComments == 1)
            {
                product.AverageRating = newReting;
                product.AmountOfComments = 1;
                await _productRepository.Update(product);
                return true;
            }
            else
            {
                product.AverageRating = (product.AverageRating * product.AmountOfComments + reting) / product.AmountOfComments;
                // востанавливаем рейтинг и прибавляем новые данные, потом делем на количество отзывом
                product.AmountOfComments = product.AmountOfComments;
                await _productRepository.Update(product);
                return true;
            }
        }

        public async Task<bool> DeleteReting(int productId, decimal reting)
        {
            Domain.Entities.Product product = await _productRepository.Get(productId);
            if (product == null)
            {
                return false;
            }
            if (product.AmountOfComments == 1)
            {
                product.AverageRating = 0;
                product.AmountOfComments = 0;
                await _productRepository.Update(product);
                return true;
            }
            else
            {
                product.AverageRating = (product.AverageRating * product.AmountOfComments - reting) / product.AmountOfComments-1;
                // востанавливаем рейтинг и прибавляем новые данные, потом делем на количество отзывом
                product.AmountOfComments = product.AmountOfComments-1;
                await _productRepository.Update(product);
                return true;
            }
        }

        public async Task<AnswerWithBackendDto<ProductDto>> GetProductsByCluster(List<int> ClusterIds)
        {
            AnswerWithBackendDto<ProductDto> answerWithBackendDto = new();
            var items = await _productRepository.GetProductsByClusters(ClusterIds);
            if (items == null)
            {
                answerWithBackendDto.AddErrorLog("Произошла ошибка при обращении к бд.");
            }
            if (items?.Count == 0)
            {
                answerWithBackendDto.AddErrorLog($"Данные отсутствуют.");
            }
            foreach (Product product in items)
            {
                answerWithBackendDto.AddObject(Adapters.ProductAdapter.ConvertToDTOProduct(product));
            }
            return answerWithBackendDto;
        }
    }
}
