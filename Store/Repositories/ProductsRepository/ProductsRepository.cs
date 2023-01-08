using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.ProductsRepository
{
    public class ProductsRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductsRepository(AppDbContext context) : base(context)
        {

        }

        public IQueryable<ProductDetailsDto> ShowProductsReport()
        {
            /* var products = _context.Products.GroupBy(n => n.Id).Include("OrderList")
                 .SelectMany(prod => prod.Select(prodLine => new ProductDetailsDto
                 {
                     Name = prodLine.Name,
                     Description = prodLine.Description,
                     Price = prodLine.Price,
                     QuantitySold = prodLine.OrderList.Sum(c => c.Quantity)
                 })).ToList();*/

            var products = from p in _context.OrderLists
                           join bp in _context.Products on p.ProductId equals bp.Id
                           select new { p, bp } into t1
                           group t1 by t1.p.ProductId into g
                           select new ProductDetailsDto
                           {
                               Name = g.FirstOrDefault().bp.Name,
                               Description = g.FirstOrDefault().bp.Description,
                               Price = g.FirstOrDefault().bp.Price,
                               QuantitySold = g.Sum(n => n.p.Quantity)
                           };
            
            return products;
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
