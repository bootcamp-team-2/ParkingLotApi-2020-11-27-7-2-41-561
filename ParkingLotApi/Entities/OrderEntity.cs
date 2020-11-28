using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace ParkingLotApi.Entities
{
    public enum OrderStatus : int
    {
        Open,
        Close
    }

    public class OrderEntity
    {
        [Key]
        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTimeOffset CreationTimeOffset { get; set; }
        public DateTimeOffset CloseTimeOffset { get; set; }
        public OrderStatus Status { get; set; }
    }
}
