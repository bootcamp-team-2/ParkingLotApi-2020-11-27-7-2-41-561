using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class OrderRequestDto
    {
        public OrderRequestDto()
        {
        }

        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
    }
}
