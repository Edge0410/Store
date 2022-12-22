using Store.Models;

namespace Store.Services.Products
{
    public interface IProductService
    {
        Task Create(Product newProduct);
        Guid FindProductByName(string name);
        Task Delete(Guid id);
    }
}
