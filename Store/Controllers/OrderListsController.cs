using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Models.DTOs;
using Store.Services.OrderLists;
using Store.Services.Products;

namespace Store.Controllers
{
    [Route("api/orderlist")]
    [ApiController]
    public class OrderListsController : ControllerBase
    {
        private IOrderListService _orderlistService;
        
        public OrderListsController(IOrderListService orderlistService)
        {
            _orderlistService = orderlistService;
        }

        [HttpPost("add-product-to-order"), Authorize]
        public async Task<IActionResult> AddProductToOrder(Guid order, Guid product, int productNumber)
        {
            var orderListToCreate = new OrderListRequestDto
            {
                OrderId = order,
                ProductId = product,
                Quantity = productNumber
            };
            await _orderlistService.Create(orderListToCreate);
            return Ok("Product added to order");
        }

        [HttpDelete("delete-product-from-order")]
        public async Task<IActionResult> DeleteProductFromOrder(Guid order, Guid product)
        {
            await _orderlistService.Delete(order, product);
            return Ok("Product " + product + " removed from order " + order);
        }

    }
}
