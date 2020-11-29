using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entity;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {
        }

        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            Name = parkingLotEntity.Name;
            Capacity = parkingLotEntity.Capacity;
            Location = parkingLotEntity.Location;
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return Equals((ParkingLotDto)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(ParkingLotDto other)
        {
            return Name == other.Name && Capacity == other.Capacity && Location == other.Location;
        }

        public bool IsValid(out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrEmpty(this.Name))
            {
                errorMessage = "name of parkingLot can not be null or empty";
                return false;
            }

            if (string.IsNullOrEmpty(this.Capacity.ToString()))
            {
                errorMessage = "capacity can not be null or empty";
                return false;
            }

            if (this.Capacity < 0)
            {
                errorMessage = "capacity can not be less than 0";
                return false;
            }

            if (string.IsNullOrEmpty(this.Location))
            {
                errorMessage = "location can not be null or empty";
                return false;
            }

            return true;
        }
    }
}
