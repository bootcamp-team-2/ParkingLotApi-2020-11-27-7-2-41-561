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

        public async Task<ParkingLotDto> GetById(int id)
        {
            var foundParkingLotEntity = await this.parkingLotDbContext.ParkingLots.FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Id == id);
            return new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<int> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = new ParkingLotEntity(parkingLotDto);
            await this.parkingLotDbContext.ParkingLots.AddAsync(parkingLotEntity);
            await this.parkingLotDbContext.SaveChangesAsync();
            return parkingLotEntity.Id;
        }

        public async Task DeleteParkingLot(string name)
        {
            var foundParkingLotEntity = await this.parkingLotDbContext.ParkingLots.FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Name == name);
            this.parkingLotDbContext.ParkingLots.Remove(foundParkingLotEntity);
            await this.parkingLotDbContext.SaveChangesAsync();
        }

        public async Task<bool> ContainExistingParkingLot(string name)
        {
            return this.parkingLotDbContext.ParkingLots.Any(item => item.Name == name);
        }
    }
}
