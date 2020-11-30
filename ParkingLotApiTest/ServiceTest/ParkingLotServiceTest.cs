using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using Xunit;

namespace ParkingLotApiTest.ServiceTest
{
    [Collection("IntegrationTest")]
    public class ParkingLotServiceTest : TestBase
    {
        public ParkingLotServiceTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        public ParkingLotDbContext GetContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            return scopedServices.GetRequiredService<ParkingLotDbContext>();
        }

        [Fact]
        public async Task Should_Add_New_Factory_Success_Via_Service()
        {
            // Given
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "ParkingLot_A";
            parkingLotDto.Capacity = 30;
            parkingLotDto.Location = "Beijing";

            ParkingLotDbContext context = GetContext();
            ParkingLotService service = new ParkingLotService(context);

            // When
            await service.AddParkingLot(parkingLotDto);

            // Then
            Assert.Equal(1, context.ParkingLots.ToList().Count);

            var firstParkingLot = await context.ParkingLots.FirstOrDefaultAsync();
            Assert.Equal(parkingLotDto.Name, firstParkingLot.Name);
            Assert.Equal(parkingLotDto.Capacity, firstParkingLot.Capacity);
            Assert.Equal(parkingLotDto.Location, firstParkingLot.Location);
        }
    }
}
