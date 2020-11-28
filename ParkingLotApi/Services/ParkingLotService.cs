using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;

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
        public async Task<string> AddAsync(ParkingLotDto parkingLotDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ParkingLotDto>> GetAllAsync(string name, int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public async Task<ParkingLotDto> GetAsync(string id)
        {
            throw new NotImplementedException();
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
