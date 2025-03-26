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
       
        public async Task<int> AddProduct(AddProductDto productDto)//int shopId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber)
        {
            Domain.Entities.Product product = Adapters.ProductAdapter.ConvertToEntity(productDto);
            if ((await _productRepository.Add(product)) is null)
            {
                return -1;
            }
            else 
            {
                return product.Id;
            }
        }

        public async Task<bool> DeleteProduct(DeleteProductDto productDto)
        {
            Domain.Entities.Product product = await _productRepository.Get(productDto.Id);
            if (product == null)
            {
                return false;   

            }
            product.IsDeleted = true;
            await _productRepository.Update(product);
            return true;
        }

        public async Task<bool> UpdateProduct(UpdateProductDto productDto) //,int ProductId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber)
        {
            Domain.Entities.Product product = Adapters.ProductAdapter.ConvertToEntity(productDto);
            Domain.Entities.Product _ = await _productRepository.Get(product.Id);
            if (_ is null) 
            { 
                return false;
            }
            product.ShopId = _.ShopId;
            await _productRepository.Update(product);
            return true;
        }

        public async Task<List<ProductDto>> GetShopProducts(GetallShopProductsDto productDto) 
        {
            List<ProductDto> products = new List<ProductDto>();
            foreach (Domain.Entities.Product product in await _productRepository.GetShopProducts(productDto.ShopId)) 
            {
                products.Add(Adapters.ProductAdapter.ConvertToDTOProduct(product));
            }
            return products;

        }

        public async Task<List<ProductDto>> GetAllProduct() 
        {
            List<ProductDto> products = new List<ProductDto>();
            foreach (Domain.Entities.Product product in await _productRepository.GetAll())
            {
                products.Add(Adapters.ProductAdapter.ConvertToDTOProduct(product));
            }
            return products;
        }


        public async Task<List<ProductDto>> GetProductsByCluster(GetAllClusterProductsDto productDto) 
        {
            List<ProductDto> products = new List<ProductDto>();
            foreach (Domain.Entities.Product product in await _productRepository.GetProductsByCluster(productDto.ClusterId)) 
            {
                products.Add(Adapters.ProductAdapter.ConvertToDTOProduct(product));
            }
            return products;
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

        public async Task<List<ProductDto>> GetProductsByCluster(List<int> ClusterIds)
        {
            List<ProductDto> products = new List<ProductDto>();
            foreach (Domain.Entities.Product product in await _productRepository.GetProductsByClusters(ClusterIds))
            {
                products.Add(Adapters.ProductAdapter.ConvertToDTOProduct(product));
            }
            return products;
        }
    }
}
