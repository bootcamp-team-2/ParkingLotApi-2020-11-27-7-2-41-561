using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entity;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Service
{
    public class ParkingLotService
    {
        private readonly ParkingLotDbContext parkingLotDbContext;

        public ParkingLotService(ParkingLotDbContext parkingLotDbContext)
        {
            this.parkingLotDbContext = parkingLotDbContext;
        }

        public async Task<ParkingLotDto> GetByNameAsync(string name)
        {
            var foundParkingLotEntity = await this.parkingLotDbContext.ParkingLots.FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Name == name);
            return new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<string> AddParkingLotAsync(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = new ParkingLotEntity(parkingLotDto);
            await this.parkingLotDbContext.ParkingLots.AddAsync(parkingLotEntity);
            await this.parkingLotDbContext.SaveChangesAsync();
            return parkingLotEntity.Name;
        }
    }
}
