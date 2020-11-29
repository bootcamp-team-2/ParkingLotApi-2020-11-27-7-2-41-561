using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        private readonly ParkingLotDbContext parkingLotDbContext;
        public ParkingLotService(ParkingLotDbContext parkingLotDbContext)
        {
            this.parkingLotDbContext = parkingLotDbContext;
        }

        public async Task<int> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = new ParkingLotEntity(parkingLotDto);
            await parkingLotDbContext.AddAsync(parkingLotEntity);
            await parkingLotDbContext.SaveChangesAsync();

            return parkingLotEntity.Id;
        }

        public async Task<ParkingLotDto> GetById(int id)
        {
            var foundParkingLotEntity = await parkingLotDbContext.ParkingLots
                .Include(parkingLot => parkingLot.Name)
                .Include(parkingLot => parkingLot.Capacity)
                .Include(parkingLot => parkingLot.Location)
                .FirstOrDefaultAsync(parkingLot => parkingLot.Id == id);
            return foundParkingLotEntity == null ? null : new ParkingLotDto(foundParkingLotEntity);
        }
    }
}
