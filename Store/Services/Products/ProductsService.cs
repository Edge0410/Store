using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.ProductsRepository;

namespace Store.Services.Products
{
    public class ProductsService : IProductService
    {
        public IProductRepository _productRepository;

        public ProductsService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Create(Product newProduct)
        {
            await _productRepository.CreateAsync(newProduct);
            await _productRepository.SaveAsync();
        }

        public Guid FindProductByName(string name)
        {
            return _productRepository.FindByName(name);
        }

        public async Task Edit(Guid id, ProductRequestDto editProduct)
        {
            var productFound = await _productRepository.FindByIdAsync(id);
            productFound.Description = editProduct.Description;
            productFound.Name = editProduct.Name;
            productFound.Price = editProduct.Price;
            productFound.Quantity = editProduct.Quantity;
            await _productRepository.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = _productRepository.FindById(id);
            _productRepository.Delete(product);
            await _productRepository.SaveAsync();
        }
    }
}
