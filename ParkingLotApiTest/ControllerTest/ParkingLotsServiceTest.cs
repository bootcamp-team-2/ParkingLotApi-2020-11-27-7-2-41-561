using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Service;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    [Collection("ServiceTest")]
    public class ParkingLotsServiceTest : TestBase
    {
        public ParkingLotsServiceTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        //[Fact]
        //public async Task Should_get_hello_world()
        //{
        //    var client = GetClient();

        //    var allCompaniesResponse = await client.GetAsync("/Hello");
        //    var responseBody = await allCompaniesResponse.Content.ReadAsStringAsync();

        //    Assert.Equal("Hello World", responseBody);
        //}

        [Fact]
        public async Task Should_create_a_new_parkingLot_when_give_name_capacity_and_location_Test()
        {
            var scope = Factory.Services.CreateScope();
            var scopeServices = scope.ServiceProvider;
            ParkingLotDbContext context = scopeServices.GetRequiredService<ParkingLotDbContext>();
            ParkingLotDto paringLotDto = new ParkingLotDto();
            paringLotDto.Name = "123";
            paringLotDto.Capacity = 0;
            paringLotDto.Location = "southRoad";

            ParkingLotService parkingLotService = new ParkingLotService(context);
            await parkingLotService.AddParkingLot(paringLotDto);

            Assert.Equal(1, context.ParkingLots.Count());
            var createdParkingLot = await context.ParkingLots.FirstOrDefaultAsync(item => item.Name == paringLotDto.Name);
            Assert.Equal(paringLotDto, new ParkingLotDto(createdParkingLot));
        }

        [Fact]
        public async Task Should_return_specific_parkingLot_when_give_name_Test()
        {
            var scope = Factory.Services.CreateScope();
            var scopeServices = scope.ServiceProvider;
            ParkingLotDbContext context = scopeServices.GetRequiredService<ParkingLotDbContext>();
            ParkingLotDto parkingLot1 = new ParkingLotDto();
            parkingLot1.Name = "345";
            parkingLot1.Capacity = 0;
            parkingLot1.Location = "southRoad";
            ParkingLotDto parkingLot2 = new ParkingLotDto();
            parkingLot2.Name = "234";
            parkingLot2.Capacity = 0;
            parkingLot2.Location = "southRoad";

            ParkingLotService parkingLotService = new ParkingLotService(context);
            var name1 = await parkingLotService.AddParkingLot(parkingLot1);
            await parkingLotService.AddParkingLot(parkingLot2);
            var requiredParkingLot = await parkingLotService.GetById(name1);
            Assert.Equal(parkingLot1, requiredParkingLot);
        }
    }
}