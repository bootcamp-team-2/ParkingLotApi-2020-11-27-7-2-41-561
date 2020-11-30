using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
            CreateTime = DateTime.Now;
        }

        public OrderDto(OrderRequestDto orderRequestDto)
        {
            OrderNumber = Guid.NewGuid();
            ParkingLotName = orderRequestDto.ParkingLotName;
            PlateNumber = orderRequestDto.PlateNumber;
            CreateTime = DateTime.Now;
        }

        public OrderDto(OrderEntity orderEntity)
        {
            OrderNumber = orderEntity.OrderNumber;
            ParkingLotName = orderEntity.ParkingLotName;
            PlateNumber = orderEntity.PlateNumber;
            CreateTime = orderEntity.CreateTime;
        }

        public Guid OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
