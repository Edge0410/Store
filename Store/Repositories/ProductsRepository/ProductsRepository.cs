using Store.Models;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.ProductsRepository
{
    public class ProductsRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductsRepository(AppDbContext context) : base(context)
        {

        }

        public Guid FindByName(string name)
        {
            var product = _table.FirstOrDefault(x => x.Name == name);
            if(product == null)
            {
                return Guid.Empty;
            }

            return product.Id;
        }
    }
}
