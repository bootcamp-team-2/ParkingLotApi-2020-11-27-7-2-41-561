using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public class CarService
    {
        private readonly ParkingLotDbContext parkingLotDbContext;

        public CarService(ParkingLotDbContext parkingLotDbContext)
        {
            this.parkingLotDbContext = parkingLotDbContext;
        }
    }
}
