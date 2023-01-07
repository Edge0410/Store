using Store.Models;
using Store.Models.DTOs;

namespace Store.Services.Products
{
    public interface IProductService
    {
        Task Create(Product newProduct);
        Guid FindProductByName(string name);
        Task Edit(Guid id, ProductRequestDto editProduct);
        Task Delete(Guid id);
    }
}
