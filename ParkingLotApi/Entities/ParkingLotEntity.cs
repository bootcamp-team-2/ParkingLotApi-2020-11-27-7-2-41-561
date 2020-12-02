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
            AvailablePosition = Capacity;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public int AvailablePosition { get; set; }
    }
}
