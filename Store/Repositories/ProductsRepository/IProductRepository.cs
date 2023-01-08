using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.ProductsRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Guid FindByName(string name);
        IQueryable<ProductDetailsDto> ShowProductsReport();
    }
}
