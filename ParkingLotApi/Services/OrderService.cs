using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public interface IOrderService
    {
        public Task<OrderDto> AddAsync(OrderCreateDto orderCreateDto);
        public Task<OrderDto> GetAsync(string orderNumber);
        public Task UpdateAsync(OrderUpdateDto orderUpdateDto);
    }

    public class OrderService : IOrderService
    {
        private readonly ParkingLotContext parkingLotContext;

        public OrderService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<OrderDto> AddAsync(OrderCreateDto orderCreateDto)
        {
            var newOrder = new OrderEntity(orderCreateDto);
            if (this.parkingLotContext.Orders.Any(order =>
                order.PlateNumber == newOrder.PlateNumber && order.Status == OrderStatus.Open))
            {
                return null;
            }

            await this.parkingLotContext.Orders.AddAsync(newOrder);
            await this.parkingLotContext.SaveChangesAsync();

            return new OrderDto(newOrder);
        }

        public async Task<OrderDto> GetAsync(string orderNumber)
        {
            var order = this.parkingLotContext.Orders.FirstOrDefault(order => order.OrderNumber == orderNumber);
            if (order == null)
            {
                return null;
            }

            return new OrderDto(order);
        }

        public async Task UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
