using Store.Models;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.ProductsRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Guid FindByName(string name);
    }
}
