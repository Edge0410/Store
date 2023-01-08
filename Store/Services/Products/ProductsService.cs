using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.ProductsRepository;
using Store.Repositories.UnitOfWork;

namespace Store.Services.Products
{
    public class ProductsService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Product newProduct)
        {
            await _unitOfWork.ProductRepository.CreateAsync(newProduct);
            await _unitOfWork.SaveAsync();
        }

        public Guid FindProductByName(string name)
        {
            return _unitOfWork.ProductRepository.FindByName(name);
        }

        public IQueryable<ProductDetailsDto> ShowProductsReport()
        {
            return _unitOfWork.ProductRepository.ShowProductsReport();
        }

        public async Task Edit(Guid id, ProductRequestDto editProduct)
        {
            var productFound = await _unitOfWork.ProductRepository.FindByIdAsync(id);
            productFound.Description = editProduct.Description;
            productFound.Name = editProduct.Name;
            productFound.Price = editProduct.Price;
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = _unitOfWork.ProductRepository.FindById(id);
            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
