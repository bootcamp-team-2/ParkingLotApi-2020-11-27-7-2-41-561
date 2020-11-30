﻿using System;
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
                .FirstOrDefaultAsync(parkingLot => parkingLot.Id == id);
            return foundParkingLotEntity == null ? null : new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<int?> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            if (parkingLotDto.Name == null || parkingLotDto.Location == null)
            {
                return null;
            }

            ParkingLotEntity parkingLotEntity = new ParkingLotEntity(parkingLotDto);
            var addedParkingLot = await parkingLotDbContext.AddAsync(parkingLotEntity);
            await parkingLotDbContext.SaveChangesAsync();

            return addedParkingLot.Entity.Id;
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {
            var parkingLots = await parkingLotDbContext.ParkingLots
                .ToListAsync();
            return parkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task DeleteParkingLot(int id)
        {
            var foundParkingLot = await parkingLotDbContext.ParkingLots
                .FirstOrDefaultAsync(parkingLot => parkingLot.Id == id);
            this.parkingLotDbContext.ParkingLots.Remove(foundParkingLot);
            await this.parkingLotDbContext.SaveChangesAsync();
        }

        public async Task<List<ParkingLotDto>> GetByPage(int pageIndex, int pageSize = 15)
        {
            var startIndex = (pageIndex - 1) * pageSize;
            var extractedParkingLots = await parkingLotDbContext.ParkingLots.Skip(startIndex).Take(pageSize)
                .Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToListAsync();
            return extractedParkingLots;
        }

        public async Task<ParkingLotDto> UpdateParkingLotCapacity(int id, ParkingLotUpdateDto parkingLotUpdateDto)
        {
            var foundParkingLot =
                await parkingLotDbContext.ParkingLots.FirstOrDefaultAsync(parkingLotDto => parkingLotDto.Id == id);
            foundParkingLot.Capacity = parkingLotUpdateDto.Capacity;
            await this.parkingLotDbContext.SaveChangesAsync();

            return new ParkingLotDto(foundParkingLot);
        }
    }
}
