using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public interface IParkingLotService
    {
        public Task<string> AddAsync(ParkingLotDto parkingLotDto);
        public Task<List<ParkingLotDto>> GetAllAsync(string name, int limit, int offset);
        public Task<ParkingLotDto> GetAsync(string id);
        public Task Delete(string id);
        public Task Update(string id, ParkingLotUpdateDto parkingLotUpdateDto);
    }

    public class ParkingLotService : IParkingLotService
    {
        private readonly ParkingLotContext parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<string> AddAsync(ParkingLotDto parkingLotDto)
        {
            var parkingLot = new ParkingLotEntity(parkingLotDto);
            if (parkingLotContext.ParkingLots.Any(p => p.Name == parkingLot.Name))
            {
                return string.Empty;
            }

            await parkingLotContext.ParkingLots.AddAsync(parkingLot);
            await parkingLotContext.SaveChangesAsync();
            return parkingLot.Id;
        }

        public async Task<List<ParkingLotDto>> GetAllAsync(string name, int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public async Task<ParkingLotDto> GetAsync(string id)
        {
            var parkingLot = await this.parkingLotContext.ParkingLots
                .FirstOrDefaultAsync(p => p.Id == id);
            if (parkingLot == null)
            {
                return null;
            }

            var returnedParkingLot = new ParkingLotDto(parkingLot);
            return returnedParkingLot;
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(string id, ParkingLotUpdateDto parkingLotUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
