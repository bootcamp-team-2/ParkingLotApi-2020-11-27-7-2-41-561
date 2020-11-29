using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Hosting;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingLotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IParkingLotService parkingLotService;
        private readonly IHostEnvironment hostEnvironment;
        public OrdersController(IOrderService orderService, IParkingLotService parkingLotService, IHostEnvironment hostEnvironment)
        {
            this.orderService = orderService;
            this.parkingLotService = parkingLotService;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddAsync(OrderCreateDto orderCreateDto)
        {
            var orderCreated = await this.orderService.AddAsync(orderCreateDto);
            if (orderCreated == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetAsync),
                new { orderNumber = orderCreated.OrderNumber }, orderCreated);
        }

        [HttpGet("{orderNumber}")]
        public async Task<ActionResult<OrderDto>> GetAsync(string orderNumber)
        {
            var searchedOrder = await this.orderService.GetAsync(orderNumber);
            if (searchedOrder == null)
            {
                return NotFound();
            }

            return Ok(searchedOrder);
        }

        [HttpGet("dev")]
        public async Task<ActionResult<IEnumerable<OrderEntity>>> GetAllOrdersInDevAsync()
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var allOrders = await this.orderService.GetAllInDevAsync();
            return Ok(allOrders);
        }

        [HttpGet("dev/{orderNumber}")]
        public async Task<ActionResult<OrderEntity>> GetOrderInDevAsync(string orderNumber)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var searchedOrder = await this.orderService.GetInDevAsync(orderNumber);
            if (searchedOrder == null)
            {
                return NotFound();
            }

            return Ok(searchedOrder);
        }

        [HttpPatch("{orderNumber}")]
        public async Task<ActionResult> UpdateAsync(string orderNumber, OrderUpdateDto orderUpdateDto)
        {
            if (orderUpdateDto.Status != OrderStatus.Close)
            {
                return BadRequest();
            }

            var orderToUpdate = await this.orderService.GetAsync(orderNumber);
            if (orderToUpdate == null)
            {
                return NotFound();
            }

            await this.orderService.UpdateAsync(orderNumber, orderUpdateDto);

            return NoContent();
        }
    }
}
