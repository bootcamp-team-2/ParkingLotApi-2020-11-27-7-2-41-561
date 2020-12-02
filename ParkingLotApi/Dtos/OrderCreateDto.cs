using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class OrderCreateDto
    {
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
    }
}
