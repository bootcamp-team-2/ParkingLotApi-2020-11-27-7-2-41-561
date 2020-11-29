using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;

namespace ParkingLotApi.Entities
{
    public enum OrderStatus : int
    {
        Open,
        Close
    }

    public class OrderEntity
    {
        public OrderEntity()
        {
        }

        public OrderEntity(OrderCreateDto orderCreateDto)
        {
            ParkingLotName = orderCreateDto.ParkingLotName;
            PlateNumber = orderCreateDto.PlateNumber;
            CreationTimeOffset = DateTimeOffset.Now;
            Status = OrderStatus.Open;
        }

        [Key] public string OrderNumber { get; set; } = Guid.NewGuid().ToString("N");
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTimeOffset CreationTimeOffset { get; set; }
        public DateTimeOffset CloseTimeOffset { get; set; }
        public OrderStatus Status { get; set; }
    }
}
