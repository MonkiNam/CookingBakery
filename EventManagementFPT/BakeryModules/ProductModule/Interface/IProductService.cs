using CookingBakery.Models;
using System;
using System.Threading.Tasks;

namespace CookingBakery.BakeryModules.ProductModule.Interface
{
    public interface IProductService
    {
        public Task<Product> AddNewProduct(Product newProduct);


        public Task<Product> GetProductById(Guid? productId);


        public Task UpdateProduct(Product productUpdate);

    }
}
