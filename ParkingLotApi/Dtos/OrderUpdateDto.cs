using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class OrderUpdateDto
    {
        public OrderUpdateDto()
        {
        }

        public OrderUpdateDto(OrderDto orderDto)
        {
            ParkingLotName = orderDto.ParkingLotName;
            PlateNumber = orderDto.PlateNumber;
            CreationTimeOffset = orderDto.CreationTimeOffset;
        }

        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTimeOffset CreationTimeOffset { get; set; }
    }
}
