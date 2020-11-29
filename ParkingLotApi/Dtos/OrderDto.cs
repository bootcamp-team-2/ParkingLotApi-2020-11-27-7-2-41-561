using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
        }

        public OrderDto(OrderEntity orderEntity)
        {
            OrderNumber = orderEntity.OrderNumber;
            ParkingLotName = orderEntity.ParkingLotName;
            PlateNumber = orderEntity.PlateNumber;
            CreationTimeOffset = orderEntity.CreationTimeOffset;
        }

        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTimeOffset CreationTimeOffset { get; set; }
    }
}
