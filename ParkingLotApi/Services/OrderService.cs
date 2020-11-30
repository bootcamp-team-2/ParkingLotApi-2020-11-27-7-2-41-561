using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public class OrderService
    {
        private readonly ParkingLotDbContext parkingLotDbContext;
        private readonly ParkingLotService parkingLotService;

        public OrderService(ParkingLotDbContext parkingLotDbContext, ParkingLotService parkingLotService)
        {
            this.parkingLotDbContext = parkingLotDbContext;
            this.parkingLotService = parkingLotService;
        }

        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            var orderEntityList = await parkingLotDbContext.Orders.ToListAsync();
            return orderEntityList.Select(orderEntity => new OrderDto(orderEntity)).ToList();
        }

        public async Task<OrderDto> GetOrderByOrderNumber(Guid orderNumber)
        {
            var orderFound =
                await parkingLotDbContext.Orders.FirstOrDefaultAsync(orderDto => orderDto.OrderNumber == orderNumber);
            return new OrderDto(orderFound);
        }

        public async Task<OrderDto?> AddOrder(OrderRequestDto orderRequestDto)
        {
            var parkingLots = await parkingLotDbContext.ParkingLots.ToListAsync();
            if (parkingLots.Any(parkingLot => parkingLot.Cars.Any(car => car.PlateNumber == orderRequestDto.PlateNumber)))
            {
                return null;
            }

            var parkingLot = parkingLots.FirstOrDefault(parkingLot => parkingLot.Name == orderRequestDto.ParkingLotName);
            if (parkingLot.Capacity > parkingLot.Cars.Count)
            {
                var carEntity = new CarEntity();
                carEntity.PlateNumber = orderRequestDto.PlateNumber;
                carEntity.ParkingLotEntity = parkingLot;
                carEntity.ParkingLotId = parkingLot.Id;
                parkingLot.Cars.Add(carEntity);

                //await parkingLotDbContext.Cars.AddAsync(carEntity);
                var orderDto = new OrderDto(orderRequestDto);
                var orderEntity = new OrderEntity(orderDto);
                orderEntity.Status = "Created";
                await this.parkingLotDbContext.Orders.AddAsync(orderEntity);

                await this.parkingLotDbContext.SaveChangesAsync();
                return orderDto;
            }

            return null;
        }
    }
}
