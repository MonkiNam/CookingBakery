using BussinessObject.Models;
using Repositories.BakeryModules.ProductModule.Interface;
using Repositories.Utils.BakeryRepository;

namespace Repositories.BakeryModules.ProductModule
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CookingBakeryContext _db;

        public ProductRepository(CookingBakeryContext db) : base(db)
        {
            _db = db;
        }
    }
}
