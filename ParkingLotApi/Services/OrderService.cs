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
        public Task<OrderEntity> GetInDevAsync(string orderNumber);
        public Task UpdateAsync(string orderNumber, OrderUpdateDto orderUpdateDto);
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
            var order = this.parkingLotContext.Orders.FirstOrDefault(_ => _.OrderNumber == orderNumber);
            if (order == null)
            {
                return null;
            }

            return new OrderDto(order);
        }

        public async Task<OrderEntity> GetInDevAsync(string orderNumber)
        {
            var order = this.parkingLotContext.Orders.FirstOrDefault(_ => _.OrderNumber == orderNumber);
            return order;
        }

        public async Task UpdateAsync(string orderNumber, OrderUpdateDto orderUpdateDto)
        {
            var orderToUpdate = this.parkingLotContext.Orders
                .FirstOrDefault(_ => _.OrderNumber == orderNumber);
            if (orderToUpdate != null)
            {
                orderToUpdate.Status = orderUpdateDto.Status;
                orderToUpdate.CloseTimeOffset = DateTimeOffset.Now;
            }

            await this.parkingLotContext.SaveChangesAsync();
        }
    }
}
