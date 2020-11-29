using System;
using System.Collections.Generic;
using System.Net;
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

        public StringContent SerializeParkingLot<T>(T parkingLot)
        {
            var httpContent = JsonConvert.SerializeObject(parkingLot);
            return new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        public async Task<T> DeSerializeResponseAsync<T>(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        //[Fact]
        //public async Task Should_create_parkingLot_success_Test()
        //{
        //    var client = GetClient();
        //    ParkingLotDto paringLotDto = new ParkingLotDto();
        //    paringLotDto.Name = "123";
        //    paringLotDto.Capacity = 0;
        //    paringLotDto.Location = "southRoad";
        //    var requestBody = SerializeParkingLot<ParkingLotDto>(paringLotDto);

        //    var response = await client.PostAsync("/ParkingLots", requestBody);
        //    var acturalParkingLot = await DeSerializeResponseAsync<ParkingLotDto>(response);

        //    Assert.Equal(paringLotDto, acturalParkingLot);
        //}

        //[Fact]
        //public async Task Should_Not_create_parkingLot_Return_Bad_Request_With_Error_Message_When_give_null_name_Test()
        //{
        //    var client = GetClient();
        //    ParkingLotDto paringLotDto = new ParkingLotDto();
        //    paringLotDto.Name = null;
        //    paringLotDto.Capacity = 0;
        //    paringLotDto.Location = "southRoad";
        //    var requestBody = SerializeParkingLot<ParkingLotDto>(paringLotDto);

        //    var response = await client.PostAsync("/ParkingLots", requestBody);
        //    var errorMessage = await response.Content.ReadAsStringAsync();
        //    var acturalParkingLot = await DeSerializeResponseAsync<ParkingLotDto>(response);

        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //    Assert.Equal("name of parkingLot can not be null or empty", errorMessage);
        //}

        //[Fact]
        //public async Task Should_Not_create_parkingLot_Return_Bad_Request_With_Error_Message_When_give_null_Location_Test()
        //{
        //    var client = GetClient();
        //    ParkingLotDto paringLotDto = new ParkingLotDto();
        //    paringLotDto.Name = "com1";
        //    paringLotDto.Capacity = 0;
        //    paringLotDto.Location = null;
        //    var requestBody = SerializeParkingLot<ParkingLotDto>(paringLotDto);

        //    var response = await client.PostAsync("/ParkingLots", requestBody);
        //    var errorMessage = await response.Content.ReadAsStringAsync();
        //    var acturalParkingLot = await DeSerializeResponseAsync<ParkingLotDto>(response);

        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //    Assert.Equal("location of parkingLot can not be null or empty", errorMessage);
        //}

        //[Fact]
        //public async Task Should_Not_create_parkingLot_Return_Bad_Request_With_Error_Message_When_give_minus_value_Capactity_Test()
        //{
        //    var client = GetClient();
        //    ParkingLotDto paringLotDto = new ParkingLotDto();
        //    paringLotDto.Name = "com2";
        //    paringLotDto.Capacity = -1;
        //    paringLotDto.Location = "southRoad";
        //    var requestBody = SerializeParkingLot<ParkingLotDto>(paringLotDto);

        //    var response = await client.PostAsync("/ParkingLots", requestBody);
        //    var errorMessage = await response.Content.ReadAsStringAsync();
        //    var acturalParkingLot = await DeSerializeResponseAsync<ParkingLotDto>(response);

        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //    Assert.Equal("capacity can not be less than 0", errorMessage);
        //}
    }
}
