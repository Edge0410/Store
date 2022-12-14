using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Models.DTOs;
using Store.Services.Orders;

namespace Store.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _ordersService;

        public OrdersController(IOrderService orderService)
        {
            _ordersService = orderService;
        }

        [HttpPost("add-order"), Authorize]
        public async Task<IActionResult> AddOrder(OrderRequestDto order, Guid id_user)
        {
            var orderToCreate = new Order
            {
                DeliveryAddress = order.DeliveryAddress,
                Description = order.Description,
                UserId = id_user
            };

            await _ordersService.Create(orderToCreate);
            return Ok("Added order successfully");
        }
    }
}
