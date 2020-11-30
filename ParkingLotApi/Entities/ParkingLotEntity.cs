using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;

namespace ParkingLotApi.Entities
{
    public class ParkingLotEntity
    {
        public ParkingLotEntity()
        {
        }

        public ParkingLotEntity(ParkingLotDto parkingLotDto)
        {
            Name = parkingLotDto.Name;
            Capacity = parkingLotDto.Capacity;
            Location = parkingLotDto.Location;
            Cars = parkingLotDto.Cars.Select(carDto => new CarEntity(carDto)).ToList();
            Orders = parkingLotDto.Orders.Select(orderDto => new OrderEntity(orderDto)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public List<CarEntity> Cars { get; set; }
        public List<OrderEntity> Orders { get; set; }
    }
}
