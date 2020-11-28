using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;

namespace ParkingLotApi.Services
{
    public interface IOrderService
    {
        public Task<OrderDto> AddAsync(OrderCreateDto orderCreateDto);
        public Task<OrderDto> GetAsync(string orderNumber);
        public Task UpdateAsync(OrderUpdateDto orderUpdateDto);
    }

    public class OrderService : IOrderService
    {
        public async Task<OrderDto> AddAsync(OrderCreateDto orderCreateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto> GetAsync(string orderNumber)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
