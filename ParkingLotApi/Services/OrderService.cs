using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public interface IOrderService
    {
        public Task<(OrderDto, string)> AddAsync(OrderCreateDto orderCreateDto);
        public Task<OrderDto> GetAsync(string orderNumber);
        public Task<OrderEntity> GetInDevAsync(string orderNumber);
        public Task<List<OrderEntity>> GetAllInDevAsync();
        public Task<OrderUpdateResultDto> UpdateAsync(string orderNumber, OrderUpdateDto orderUpdateDto);
    }

    public class OrderService : IOrderService
    {
        private readonly ParkingLotContext parkingLotContext;

        public OrderService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<(OrderDto, string)> AddAsync(OrderCreateDto orderCreateDto)
        {
            var parkingLot = this.parkingLotContext.ParkingLots
                .FirstOrDefault(_ => _.Name == orderCreateDto.ParkingLotName);
            if (parkingLot == null)
            {
                return (null, "the parking lot not found");
            }

            if (parkingLot.AvailablePosition <= 0)
            {
                return (null, "the parking lot is full");
            }

            var newOrder = new OrderEntity(orderCreateDto);
            if (this.parkingLotContext.Orders.Any(order =>
                order.PlateNumber == newOrder.PlateNumber && order.Status == OrderStatus.Open))
            {
                return (null, "the car is already parked, check plate number");
            }

            await this.parkingLotContext.Orders.AddAsync(newOrder);
            parkingLot.AvailablePosition -= 1;
            await this.parkingLotContext.SaveChangesAsync();

            return (new OrderDto(newOrder), string.Empty);
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

        public async Task<List<OrderEntity>> GetAllInDevAsync()
        {
            var orders = this.parkingLotContext.Orders.ToList();
            return orders;
        }

        public async Task<OrderUpdateResultDto> UpdateAsync(string orderNumber, OrderUpdateDto orderUpdateDto)
        {
            var orderToUpdate = this.parkingLotContext.Orders
                .FirstOrDefault(_ => _.OrderNumber == orderNumber);
            if (orderToUpdate != null 
                && orderToUpdate.Status == OrderStatus.Open 
                && orderToUpdate.ParkingLotName == orderUpdateDto.ParkingLotName
                && orderToUpdate.PlateNumber == orderUpdateDto.PlateNumber
                && orderToUpdate.CreationTimeOffset == orderUpdateDto.CreationTimeOffset)
            {
                orderToUpdate.Status = OrderStatus.Close;
                orderToUpdate.CloseTimeOffset = DateTimeOffset.Now;

                var parkingLot = this.parkingLotContext.ParkingLots
                    .First(_ => _.Name == orderToUpdate.ParkingLotName);
                parkingLot.AvailablePosition += 1;
            }

            await this.parkingLotContext.SaveChangesAsync();

            return new OrderUpdateResultDto() { OrderStatus = orderToUpdate.Status };
        }
    }
}
