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

        [HttpGet("find-order"), Authorize]
        public IActionResult FindOrder(Guid id)
        {
            var order = _ordersService.FindById(id);
            //if (orderId == Guid.Empty)
            //    return BadRequest("Order was not found in the database");

            return Ok(order);
        }

        [HttpGet("show-details"), Authorize]
        public IActionResult ShowDetails(Guid id)
        {
            var order = _ordersService.ShowDetails(id);
            return Ok(order);
        }

        [HttpPut("edit-order"), Authorize]
        public async Task<IActionResult> Edit(Guid id, OrderRequestDto editOrder)
        {
            await _ordersService.Edit(id, editOrder);
            return Ok("Order was modified");
        }

        [HttpDelete("delete-order"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ordersService.Delete(id);
            return Ok("Order with id " + id + " was deleted");
        }


    }
}
