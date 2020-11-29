using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
     public class ParkingLotServiceTest : TestBase
    {
        public ParkingLotServiceTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parkingLot_successfully_via_parkingLot_service()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            ParkingLotContext parkingLotContext = scopedServices.GetRequiredService<ParkingLotContext>();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = "Liverpool";
            parkingLotDto.Capacity = 100;
            ParkingLotService parkingLotService = new ParkingLotService(parkingLotContext);
            await parkingLotService.AddParkingLot(parkingLotDto);
            Assert.Equal(1, parkingLotContext.ParkingLots.Count());
        }
    }
}
