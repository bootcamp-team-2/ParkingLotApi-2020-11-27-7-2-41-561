using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {
        }

        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            Name = parkingLotEntity.Name;
            Capacity = parkingLotEntity.Capacity;
            Location = parkingLotEntity.Location;
            Cars = parkingLotEntity.Cars.Select(carEntity => new CarDto(carEntity)).ToList();
            Orders = parkingLotEntity.Orders.Select(orderEntity => new OrderDto(orderEntity)).ToList();
        }

        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Capacity should not be negative.")]
        public int Capacity { get; set; }
        public string Location { get; set; }
        public List<CarDto> Cars { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
