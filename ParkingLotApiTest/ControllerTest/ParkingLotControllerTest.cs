using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Add_New_Factory_Success()
        {
            var client = GetClient();

            ParkingLotDto parkignLotDto = new ParkingLotDto();
            parkignLotDto.Name = "ParkingLot_A";
            parkignLotDto.Capacity = 30;
            parkignLotDto.Location = "Beijing";

            var httpContent = JsonConvert.SerializeObject(parkignLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkingLots", content);

            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ParkingLotDbContext>();
            Assert.Equal(1, context.ParkingLots.ToList().Count);

            var firstParkingLot = await context.ParkingLots.FirstOrDefaultAsync();
            Assert.Equal(parkignLotDto.Name, firstParkingLot.Name);
            Assert.Equal(parkignLotDto.Capacity, firstParkingLot.Capacity);
            Assert.Equal(parkignLotDto.Location, firstParkingLot.Location);
        }
    }
}