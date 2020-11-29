using System;
using System.Collections.Generic;
using System.Linq;
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
        private const string RootUri = "api/parkinglots";
        public ParkingLotsControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parking_lot_when_add_new()
        {
            var client = GetClient();
            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);

            var createResponse = await client.PostAsync(RootUri, parkingLotContent);

            createResponse.EnsureSuccessStatusCode();
            var returnedParkingLot = await DeserializeResponseBodyAsync<ParkingLotDto>(createResponse);
            Assert.Equal(parkingLot, returnedParkingLot);

            var getResponse = await client.GetAsync(createResponse.Headers.Location);
            getResponse.EnsureSuccessStatusCode();
            var newParkingLot = await DeserializeResponseBodyAsync<ParkingLotDto>(getResponse);
            Assert.Equal(parkingLot, newParkingLot);
        }

        [Fact]
        public async Task Should_return_parking_lot_when_get_by_id()
        {
            var client = GetClient();
            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);

            var createResponse = await client.PostAsync(RootUri, parkingLotContent);
            createResponse.EnsureSuccessStatusCode();
            var getResponse = await client.GetAsync(createResponse.Headers.Location);

            getResponse.EnsureSuccessStatusCode();
            var newParkingLot = await DeserializeResponseBodyAsync<ParkingLotDto>(getResponse);
            Assert.Equal(parkingLot, newParkingLot);
        }

        [Fact]
        public async Task Should_return_all_parking_lot_when_get_all()
        {
            var client = GetClient();
            var parkingLots = SeedParkingLots();
            foreach (var lot in parkingLots)
            {
                var content = SerializeRequestBody(lot);
                await client.PostAsync(RootUri, content);
            }

            var getResponse = await client.GetAsync(RootUri);

            getResponse.EnsureSuccessStatusCode();
            var returnedParkingLots = await DeserializeResponseBodyAsync<List<ParkingLotDto>>(getResponse);
            Assert.Equal(parkingLots, returnedParkingLots);
        }

        private StringContent SerializeRequestBody(object obj)
        {
            var httpContent = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, "application/json");
            return content;
        }

        private async Task<T> DeserializeResponseBodyAsync<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        private ParkingLotDto SeedParkingLot()
        {
            return new ParkingLotDto()
            {
                Name = "first",
                Capacity = 10,
                Location = "here",
            };
        }

        private List<ParkingLotDto> SeedParkingLots()
        {
            return new List<ParkingLotDto>()
            {
                new ParkingLotDto()
                {
                    Name = "first",
                    Capacity = 10,
                    Location = "here",
                },
                new ParkingLotDto()
                {
                    Name = "second",
                    Capacity = 20,
                    Location = "there",
                },
            };
        }
    }
}
