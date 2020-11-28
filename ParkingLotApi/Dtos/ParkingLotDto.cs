using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

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
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is ParkingLotDto))
            {
                return false;
            }

            return Equals(this, obj as ParkingLotDto);
        }

        public bool Equals(ParkingLotDto lotA, ParkingLotDto lotB)
        {
            return lotA.Name == lotB.Name && lotA.Capacity == lotB.Capacity && lotA.Location == lotB.Location;
        }
    }
}
