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
    [Collection("ControllerTests")]
    public class ParkingLotsControllerTests : TestBase
    {
        public ParkingLotsControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parking_lot_when_add_new()
        {
            var client = GetClient();
            var parkingLot = SeedData();
            var parkingLotContent = SerializeRequestBody(parkingLot);

            var createResponse = await client.PostAsync($"/api/parkinglots", parkingLotContent);

            createResponse.EnsureSuccessStatusCode();
            var returnedParkingLot = await DeserializeResponseBody(createResponse);
            Assert.Equal(parkingLot, returnedParkingLot);

            var getResponse = await client.GetAsync(createResponse.Headers.Location);
            getResponse.EnsureSuccessStatusCode();
            var newParkingLot = await DeserializeResponseBody(getResponse);
            Assert.Equal(parkingLot, newParkingLot);
        }

        [Fact]
        public async Task Should_return_parking_lot_when_get_by_id()
        {
            var client = GetClient();
            var parkingLot = SeedData();
            var parkingLotContent = SerializeRequestBody(parkingLot);

            var createResponse = await client.PostAsync($"/api/parkinglots", parkingLotContent);
            createResponse.EnsureSuccessStatusCode();
            var getResponse = await client.GetAsync(createResponse.Headers.Location);

            getResponse.EnsureSuccessStatusCode();
            var newParkingLot = await DeserializeResponseBody(getResponse);
            Assert.Equal(parkingLot, newParkingLot);
        }

        private StringContent SerializeRequestBody(ParkingLotDto parkingLotDto)
        {
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, "application/json");
            return content;
        }

        private async Task<ParkingLotDto> DeserializeResponseBody(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ParkingLotDto>(responseString);
        }

        private ParkingLotDto SeedData()
        {
            return new ParkingLotDto()
            {
                Name = "first",
                Capacity = 10,
                Location = "here",
            };
        }
    }
}
