using CookingBakery.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingBakery.BakeryModules.ProductModule.Interface
{
    public interface IProductService
    {
        public Task<Product> AddNewProduct(Product newProduct);


        public Product GetProductById(Guid? productId);


        public Task UpdateProduct(Product productUpdate);

        public ICollection<Product> GetAll();


    }
}
