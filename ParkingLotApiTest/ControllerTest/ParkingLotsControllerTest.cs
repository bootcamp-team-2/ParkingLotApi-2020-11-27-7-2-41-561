using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    [Collection("ControllerTest")]
    public class ParkingLotsControllerTest : TestBase
    {
        public ParkingLotsControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parkingLot_success_Test()
        {
            var client = GetClient();
            ParkingLotDto paringLotDto = new ParkingLotDto();
            paringLotDto.Name = "123";
            paringLotDto.Capacity = 0;
            paringLotDto.Location = "southRoad";

            var httpContent = JsonConvert.SerializeObject(paringLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var created = await client.PostAsync("/ParkingLotsApi/parkingLots", content);
            var createdBody = await created.Content.ReadAsStringAsync();
            var createdParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(createdBody);

            var parkingLots = await client.GetAsync("/ParkingLotsApi/parkingLots?name");
            var body = await parkingLots.Content.ReadAsStringAsync();
            var createdParkingLots = JsonConvert.DeserializeObject<ParkingLotDto>(body);

            Assert.Equal(paringLotDto, createdParkingLot);
        }
    }
}
