using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;

namespace ParkingLotApi.Entities
{
    public class OrderEntity
    {
        public OrderEntity()
        {
        }

        public OrderEntity(OrderDto orderDto)
        {
            OrderNumber = orderDto.OrderNumber;
            ParkingLotName = orderDto.ParkingLotName;
            PlateNumber = orderDto.PlateNumber;
            CreateTime = orderDto.CreateTime;
        }

        public Guid OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Status { get; set; }
    }
}
