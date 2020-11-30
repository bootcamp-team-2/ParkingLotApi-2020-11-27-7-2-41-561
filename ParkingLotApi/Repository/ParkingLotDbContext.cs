using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Entity;

namespace ParkingLotApi.Repository
{
    public class ParkingLotDbContext : DbContext
    {
        public ParkingLotDbContext(DbContextOptions<ParkingLotDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingLotEntity> ParkingLots { get; set; }
    }
}