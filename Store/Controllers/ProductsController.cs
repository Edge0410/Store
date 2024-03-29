﻿using Microsoft.AspNetCore.Mvc;
using Store.Models.Enums;
using Store.Models;
using Store.Models.DTOs;
using Store.Services.Products;
using Microsoft.AspNetCore.Authorization;

namespace Store.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productsService;
        public ProductsController(IProductService productService)
        {
            _productsService = productService;
        }

        [HttpPost("add-product"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(ProductRequestDto product)
        {
            var productToCreate = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };

            await _productsService.Create(productToCreate);
            return Ok("Added product successfully");

        // de adaugat service, dto, model si repo
        }

        [HttpGet("find-product"), Authorize]
        public IActionResult FindProduct(string name)
        {
            Guid productId = _productsService.FindProductByName(name);
            if (productId == Guid.Empty)
                return BadRequest("Product was not found in the database");

            return Ok("Product found! Id: " + productId);
        }

        [HttpGet("show-report")]
        public IActionResult ShowReport()
        {
            var products = _productsService.ShowProductsReport();
            return Ok(products);
        }

        [HttpPut("edit-product"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(Guid id, ProductRequestDto editProduct)
        {
            await _productsService.Edit(id, editProduct);
            return Ok("Product was modified");
        }

        [HttpDelete("delete-product"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productsService.Delete(id);
            return Ok("Product with id " + id + " was deleted");
        }

    }
}
