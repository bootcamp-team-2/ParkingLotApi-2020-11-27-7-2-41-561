using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parkingLot_successfully()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = "Liverpool";
            parkingLotDto.Capacity = 100;

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkingLots", content);

            var allParkingLotResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotResponse.Content.ReadAsStringAsync();

            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(1, returnParkingLots.Count);
        }

        [Fact]
        public async Task Should_not_create_parkingLot_and_return_error_message_if_name_exists()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = "Liverpool";
            parkingLotDto.Capacity = 100;
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkingLots", content);
            var response = await client.PostAsync("/parkingLots", content);

            var allParkingLotResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotResponse.Content.ReadAsStringAsync();
            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(1, returnParkingLots.Count);
            Assert.Equal(StatusCodes.Status409Conflict, (int)response.StatusCode);
        }

        [Fact]
        public async Task Should_not_create_parkingLot_and_return_error_message_if_name_is_null()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = null;
            parkingLotDto.Location = "Liverpool";
            parkingLotDto.Capacity = 100;
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkingLots", content);
            var allParkingLotResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotResponse.Content.ReadAsStringAsync();
            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(0, returnParkingLots.Count);
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
        }

        [Fact]
        public async Task Should_not_create_parkingLot_and_return_error_message_if_location_is_null()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = null;
            parkingLotDto.Capacity = 100;
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkingLots", content);
            var allParkingLotResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotResponse.Content.ReadAsStringAsync();
            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(0, returnParkingLots.Count);
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
        }

        [Fact]
        public async Task Should_not_create_parkingLot_and_return_error_message_if_capacity_is_less_than_0()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = "2222";
            parkingLotDto.Capacity = -2;
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkingLots", content);
            var allParkingLotResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotResponse.Content.ReadAsStringAsync();
            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(0, returnParkingLots.Count);
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
        }

        [Fact]
        public async Task Should_remove_parkingLot_successfully()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = "Liverpool";
            parkingLotDto.Capacity = 100;
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkingLots", content);
            await client.DeleteAsync(response.Headers.Location);
            var allParkingLotResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotResponse.Content.ReadAsStringAsync();
            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            Assert.Equal(0, returnParkingLots.Count);
        }

        [Fact]
        public async Task Should_get_parkingLots_on_one_page_according_to_page_size_and_start_page()
        {
            var client = GetClient();

            for (int i = 0; i < 20; i++)
            {
                ParkingLotDto parkingLotDto = new ParkingLotDto();
                parkingLotDto.Name = "LiverpoolLot" + $"{i}";
                parkingLotDto.Location = "Liverpool";
                parkingLotDto.Capacity = 100;
                var httpContent = JsonConvert.SerializeObject(parkingLotDto);
                StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
                await client.PostAsync("/parkingLots", content);
            }

            var allParkingLotResponse = await client.GetAsync("/parkingLots");
            var body1 = await allParkingLotResponse.Content.ReadAsStringAsync();

            var returnAllParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body1);

            Assert.Equal(20, returnAllParkingLots.Count);
            var parkingLotsOnOnePageResponse = await client.GetAsync("/parkinglots?pagesize=15&startpage=1");
            var body = await parkingLotsOnOnePageResponse.Content.ReadAsStringAsync();

            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(15, returnParkingLots.Count);
        }

        [Fact]
        public async Task Should_update_parkingLot_capacity_successfully()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = "Liverpool";
            parkingLotDto.Capacity = 100;
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var postResponse = await client.PostAsync("/parkingLots", content);

            UpdateParkingLotDto updateParkingLotDto = new UpdateParkingLotDto();
            updateParkingLotDto.Capacity = 300;
            var httpContent2 = JsonConvert.SerializeObject(updateParkingLotDto);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PatchAsync(postResponse.Headers.Location, content2);

            var updateParkingLotResponse = await client.GetAsync(postResponse.Headers.Location);
            var body = await updateParkingLotResponse.Content.ReadAsStringAsync();
            var returnUpdateParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);
            Assert.Equal(300, returnUpdateParkingLot.Capacity);
        }

        [Fact]
        public async Task Should_not_update_parkingLot_capacity_if_it_is_less_than_zero()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = "Liverpool";
            parkingLotDto.Capacity = 100;
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var postResponse = await client.PostAsync("/parkingLots", content);

            UpdateParkingLotDto updateParkingLotDto = new UpdateParkingLotDto();
            updateParkingLotDto.Capacity = -2;
            var httpContent2 = JsonConvert.SerializeObject(updateParkingLotDto);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            var patchResponse = await client.PatchAsync(postResponse.Headers.Location, content2);

            var updateParkingLotResponse = await client.GetAsync(postResponse.Headers.Location);
            var body = await updateParkingLotResponse.Content.ReadAsStringAsync();
            var returnUpdateParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);
            Assert.Equal(100, returnUpdateParkingLot.Capacity);
            Assert.Equal(StatusCodes.Status400BadRequest, (int)patchResponse.StatusCode);
        }

        [Fact]
        public async Task Should_get_specific_parkingLot_successfully()
        {
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto();
            parkingLotDto.Name = "LiverpoolLot";
            parkingLotDto.Location = "Liverpool";
            parkingLotDto.Capacity = 100;
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var postResponse = await client.PostAsync("/parkingLots", content);

            var response = await client.GetAsync(postResponse.Headers.Location);
            var body = await response.Content.ReadAsStringAsync();
            var returnParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);
            Assert.Equal(100, returnParkingLot.Capacity);
            Assert.Equal("Liverpool", returnParkingLot.Location);
            Assert.Equal("LiverpoolLot", returnParkingLot.Name);
        }
    }
}