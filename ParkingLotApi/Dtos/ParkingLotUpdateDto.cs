using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotUpdateDto
    {
        public ParkingLotUpdateDto()
        {
        }

        [Range(0, int.MaxValue, ErrorMessage = "Capacity should not be negative.")]
        public int Capacity { get; set; }
    }
}
