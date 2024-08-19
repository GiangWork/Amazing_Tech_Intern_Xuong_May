﻿using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.OrderModelView;
using XuongMay.ModelViews.PaginationModelView;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create_Order")]
        public async Task<IActionResult> CreateOrder([FromQuery] OrderModelView request)
        {
            var Order = await _orderService.CreateOrder(request);
            return Ok(Order);
        }

        [HttpGet("get_AllOrders")]
        public async Task<IActionResult> GetAllOrder([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1;
            var pageSize = request.pageSize ?? 2;
            var Orders = await _orderService.GetAllOrders(pageNumber, pageSize);
            return Ok(Orders);
        }

        [HttpGet("get_OrderById/{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var Order = await _orderService.GetOrderById(id);
            if (Order == null)
            {
                return NotFound();
            }
            return Ok(Order);
        }

        [HttpPut("update_Order/{id}")]
        public async Task<IActionResult> UpdateOrder(string id, [FromQuery] OrderModelView request)
        {
            var Order = await _orderService.UpdateOrder(id, request);
            if (Order == null)
            {
                return NotFound();
            }
            return Ok(Order);
        }

        [HttpDelete("delete_Order/{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
