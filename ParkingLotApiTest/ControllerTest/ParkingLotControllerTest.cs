using System;
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
        public async Task Should_Add_New_Parkinglot_Success()
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
            var allParkingLotsResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var returnCompanies = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(1, returnCompanies.Count);
        }

        [Fact]
        public async Task Should_Delete_Parkinglot_Success()
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

        [Fact]
        public async Task Should_Get_ParkingLot_InPages_Success()
        {
            // Given
            var client = GetClient();

            var parkingLotList = GenerateParkingLot(40);
            var addActionList = parkingLotList.Select(parkingLot =>
            {
                var httpContent = JsonConvert.SerializeObject(parkingLot);
                StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
                var response = client.PostAsync("/parkingLots", content);
                return response;
            });

            // When
            int pageSize = 15;
            int totalPage = addActionList.Count() / pageSize;
            int pageIndex = 1;

            var parkingLotsInPages = await client.GetAsync($"/parkingLots/{pageIndex}&{pageSize}");
            var body = await parkingLotsInPages.Content.ReadAsStringAsync();
            List<ParkingLotDto> parkingLotsReceived = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            // Then
            Assert.Equal(pageSize, parkingLotsReceived.Count);
        }

        [Fact]
        public async Task Should_Get_Parkinglot_By_Id_Success()
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
            var allParkingLotsResponse = await client.GetAsync(response.Headers.Location);
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();

            var returnParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);

            Assert.Equal(parkingLotDto.Name, returnParkingLot.Name);
            Assert.Equal(parkingLotDto.Capacity, returnParkingLot.Capacity);
            Assert.Equal(parkingLotDto.Location, returnParkingLot.Location);
        }

        [Fact]
        public async Task Should_Update_Parkinglot_Capacity_Success()
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
            var updateParkingLot = new ParkingLotUpdateDto();
            updateParkingLot.Capacity = 50;
            var updateHttpContent = JsonConvert.SerializeObject(updateParkingLot);
            StringContent updateContent = new StringContent(updateHttpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var allParkingLotsResponse = await client.PatchAsync(response.Headers.Location, updateContent);

            // Then
            var searchResponse = await client.GetAsync(response.Headers.Location);
            var searchBody = await searchResponse.Content.ReadAsStringAsync();
            var searchParkingLotDto = JsonConvert.DeserializeObject<ParkingLotDto>(searchBody);

            Assert.Equal(updateParkingLot.Capacity, searchParkingLotDto.Capacity);
        }

        public List<ParkingLotDto> GenerateParkingLot(int count)
        {
            var arrayGenerated = Enumerable.Range(0, count).ToList();
            var parkingLotList = arrayGenerated.Select(index =>
            {
                var newLot = new ParkingLotDto();
                newLot.Name = "ParkingLot_" + index;
                newLot.Capacity = index + 1;
                newLot.Location = "District_" + index;
                return newLot;
            }).ToList();
            return parkingLotList;
        }
    }
}