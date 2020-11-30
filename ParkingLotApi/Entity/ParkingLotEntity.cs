using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;

namespace ParkingLotApi.Entity
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
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
    }
}
