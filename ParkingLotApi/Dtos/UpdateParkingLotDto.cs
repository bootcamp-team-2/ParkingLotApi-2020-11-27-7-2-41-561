using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class UpdateParkingLotDto
    {
        public UpdateParkingLotDto()
        {
        }

        public UpdateParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            this.Capacity = parkingLotEntity.Capacity;
        }

        public int Capacity { get; set; }
    }
}
