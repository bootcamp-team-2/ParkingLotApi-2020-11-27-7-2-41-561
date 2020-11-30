using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            return await orderService.GetAllOrders();
        }

        [HttpGet("{orderNumber}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid orderNumber)
        {
            var orderDto = await orderService.GetOrderByOrderNumber(orderNumber);
            return Ok(orderDto);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddOrder(OrderRequestDto orderRequestDto)
        {
            OrderDto? orderDto = await orderService.AddOrder(orderRequestDto);
            if (orderDto == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetOrderById), new { orderNumber = orderDto.OrderNumber }, orderDto);
        }
    }
}
