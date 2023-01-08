using Store.Models;
using Store.Models.DTOs;

namespace Store.Services.Products
{
    public interface IProductService
    {
        Task Create(Product newProduct);
        Guid FindProductByName(string name);
        IQueryable<ProductDetailsDto> ShowProductsReport();
        Task Edit(Guid id, ProductRequestDto editProduct);
        Task Delete(Guid id);
    }
}
