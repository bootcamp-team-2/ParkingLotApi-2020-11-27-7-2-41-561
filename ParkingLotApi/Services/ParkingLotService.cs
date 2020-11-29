using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ParkingLotDto> GetById(int id)
        {
            var foundParkingLotEntity = await parkingLotDbContext.ParkingLots
                .Include(parkingLot => parkingLot.Name)
                .Include(parkingLot => parkingLot.Capacity)
                .Include(parkingLot => parkingLot.Location)
                .FirstOrDefaultAsync(parkingLot => parkingLot.Id == id);
            return foundParkingLotEntity == null ? null : new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<int> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = new ParkingLotEntity(parkingLotDto);
            await parkingLotDbContext.AddAsync(parkingLotEntity);
            await parkingLotDbContext.SaveChangesAsync();

            return parkingLotEntity.Id;
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {
            var parkingLots = await parkingLotDbContext.ParkingLots
                //.Include(parkingLot => parkingLot.Name)
                //.Include(parkingLot => parkingLot.Capacity)
                //.Include(parkingLot => parkingLot.Location)
                .ToListAsync();
            return parkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task DeleteParkingLot(int id)
        {
            var foundParkingLot = await parkingLotDbContext.ParkingLots
                //.Include(parkingLot => parkingLot.Name)
                //.Include(parkingLot => parkingLot.Capacity)
                //.Include(parkingLot => parkingLot.Location)
                .FirstOrDefaultAsync(parkingLot => parkingLot.Id == id);
            this.parkingLotDbContext.ParkingLots.Remove(foundParkingLot);
            await this.parkingLotDbContext.SaveChangesAsync();
        }

        public async Task<List<ParkingLotDto>> GetByPage(int pageIndex, int pageSize = 15)
        {
            var parkingLots = await parkingLotDbContext.ParkingLots.ToListAsync();
            var startIndex = (pageIndex - 1) * pageSize;
            return parkingLots.GetRange(startIndex, pageSize)
                .Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }
    }
}
