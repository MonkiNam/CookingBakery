using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Repositories.BakeryModules.ProductModule.Interface;
using BussinessObject.Models;

namespace Repositories.BakeryModules.ProductModule
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddNewProduct(Product newProduct)
        {
            await _productRepository.AddAsync(newProduct);
            return await GetProductById(newProduct.ProductId);
        }

        private async Task<Product> GetProductById(Guid? productId)
        {
            return await _productRepository
                .GetFirstOrDefaultAsync(
                    x => x.ProductId.Equals(productId)
                );
        }

        public async Task UpdateProduct(Product productUpdate)
        {
            await _productRepository.UpdateAsync(productUpdate);
        }

        Task<Product> IProductService.GetProductById(Guid? productId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetAll()
        {
            ICollection<Product> products =  _productRepository.GetAll();
            return products;
        }
    }
}
