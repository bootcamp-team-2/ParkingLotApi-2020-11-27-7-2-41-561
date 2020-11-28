using System;
using System.Collections.Generic;
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
        private readonly ParkingLotContext parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {
            var parkingLots = await this.parkingLotContext.ParkingLots.ToListAsync();
            return parkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task<ParkingLotDto> GetById(int id)
        {
            var foundParkingLotEntity = await this.parkingLotContext.ParkingLots
                .FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Id == id);
            return new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<string> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = new ParkingLotEntity()
            {
                Name = parkingLotDto.Name, 
                Location = parkingLotDto.Location,
                Capacity = parkingLotDto.Capacity,
            };
            await this.parkingLotContext.ParkingLots.AddAsync(parkingLotEntity);
            await this.parkingLotContext.SaveChangesAsync();
            return parkingLotEntity.Name;
        }

        public async Task<bool> IsParkingLotNameExisted(ParkingLotDto parkingLotDto)
        {
            return this.parkingLotContext.ParkingLots.Any(parkingLot => parkingLot.Name == parkingLotDto.Name);
        }
    }
}
