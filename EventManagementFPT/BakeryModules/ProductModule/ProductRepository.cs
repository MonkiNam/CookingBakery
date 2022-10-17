using CookingBakery.Models;
using CookingBakery.BakeryModules.ProductModule.Interface;
using CookingBakery.Utils.BakeryRepository;

namespace CookingBakery.BakeryModules.ProductModule
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
