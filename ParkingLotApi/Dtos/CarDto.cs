using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class CarDto
    {
        public CarDto()
        {
        }

        public CarDto(CarEntity carEntity)
        {
            PlateNumber = carEntity.PlateNumber;
        }

        public string PlateNumber { get; set; }
    }
}
