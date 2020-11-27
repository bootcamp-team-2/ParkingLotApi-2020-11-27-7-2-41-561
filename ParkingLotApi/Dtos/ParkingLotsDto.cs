using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotsDto
    {
        public ParkingLotsDto()
        {
        }

        public ParkingLotsDto(string name, int capacity, string location)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.Location = location;
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }

    }
}
