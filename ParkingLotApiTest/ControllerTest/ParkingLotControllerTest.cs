using System.Collections.Generic;
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
    [Collection("IntegrationTest")]
    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Add_New_Factory_Success()
        {
            // Given
            var client = GetClient();

            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "ParkingLot_A";
            parkingLotDto.Capacity = 30;
            parkingLotDto.Location = "Beijing";

            // When
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            // Then
            var response = await client.PostAsync("/parkingLots", content);
            await client.DeleteAsync(response.Headers.Location);
            var allParkingLotsResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var returnCompanies = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(1, returnCompanies.Count);
        }

        [Fact]
        public async Task Should_Delete_Factory_Success()
        {
            // Given
            var client = GetClient();

            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "ParkingLot_A";
            parkingLotDto.Capacity = 30;
            parkingLotDto.Location = "Beijing";

            // When
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkingLots", content);
            await client.DeleteAsync(response.Headers.Location);

            // Then
            var allParkingLots = await client.GetAsync("/parkingLots");
            var body = await allParkingLots.Content.ReadAsStringAsync();
            var parkingLotsReceived = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            //Assert.Equal(0, parkingLotsReceived.Count);
            Assert.Empty(parkingLotsReceived);
        }
    }
}