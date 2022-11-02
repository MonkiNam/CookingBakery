using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CookingBakery.BakeryModules.ProductModule.Interface;
using CookingBakery.Models;

namespace CookingBakery.BakeryModules.ProductModule
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
            return  GetProductById(newProduct.ProductId);
        }

        public Product GetProductById(Guid? productId)
        {
            return _productRepository.GetFirstOrDefaultAsync(x => x.ProductId.Equals(productId)).Result;
        }

        public async Task UpdateProduct(Product productUpdate)
        {
            await _productRepository.UpdateAsync(productUpdate);
        }

        public ICollection<Product> GetAll()
        {
            ICollection<Product> products =  _productRepository.GetAll();
            return products;
        }

    }
}
