using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;

namespace ParkingLotApi.Entities
{
    public class CarEntity
    {
        public CarEntity()
        {
        }

        public CarEntity(CarDto carDto)
        {
            PlateNumber = carDto.PlateNumber;
        }

        public string PlateNumber { get; set; }
        public ParkingLotEntity ParkingLotEntity { get; set; }
        [ForeignKey("ParkingLotForeignKey")]
        public int ParkingLotId { get; set; }
    }
}
