﻿using System;
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
using static ParkingLotApiTest.TestTool;

namespace ParkingLotApiTest.ControllerTest
{
    [Collection("ControllerTests")]
    public class ParkingLotsControllerTests : TestBase
    {
        private const string RootUri = "api/parkinglots";
        private const string RootUriForOrders = "api/orders";
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
        public async Task Should_not_create_parking_lot_when_add_new_parking_lot_with_negative_capacity()
        {
            var client = GetClient();
            var parkingLot = SeedParkingLot();
            parkingLot.Capacity = -1;
            var parkingLotContent = SerializeRequestBody(parkingLot);

            var createResponse = await client.PostAsync(RootUri, parkingLotContent);

            Assert.False(createResponse.IsSuccessStatusCode);

            var getResponse = await client.GetAsync(RootUri);
            getResponse.EnsureSuccessStatusCode();
            var allParkingLots = await DeserializeResponseBodyAsync<List<ParkingLotDto>>(getResponse);
            Assert.Empty(allParkingLots);
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
            Assert.Equal(2, returnedParkingLots.Count);
            Assert.Equal(parkingLots[0], returnedParkingLots.First(lot => lot.Name == "first"));
            Assert.Equal(parkingLots[1], returnedParkingLots.First(lot => lot.Name == "second"));
        }

        [Fact]
        public async Task Should_return_parking_lots_in_pages_when_get_using_paging_query()
        {
            var client = GetClient();
            var parkingLots = SeedParkingLotsForPagingTest();
            foreach (var lot in parkingLots)
            {
                var content = SerializeRequestBody(lot);
                await client.PostAsync(RootUri, content);
            }

            var getResponse = await client.GetAsync($"{RootUri}?pageIndex=1");

            getResponse.EnsureSuccessStatusCode();
            var returnedParkingLots = await DeserializeResponseBodyAsync<List<ParkingLotDto>>(getResponse);
            Assert.Equal(15, returnedParkingLots.Count);
            Assert.Equal(parkingLots[0], returnedParkingLots[0]);
            Assert.Equal(parkingLots[1], returnedParkingLots[1]);

            var getResponse2 = await client.GetAsync($"{RootUri}?pageIndex=5");

            getResponse2.EnsureSuccessStatusCode();
            var returnedParkingLots2 = await DeserializeResponseBodyAsync<List<ParkingLotDto>>(getResponse2);
            Assert.Empty(returnedParkingLots2);
        }

        [Fact]
        public async Task Should_update_parking_lot_capacity_when_patch_update()
        {
            var client = GetClient();
            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);
            var createResponse = await client.PostAsync(RootUri, parkingLotContent);
            createResponse.EnsureSuccessStatusCode();

            var lotUpdate = new ParkingLotUpdateDto()
            {
                Capacity = 100,
            };
            var updateContent = SerializeRequestBody(lotUpdate);
            var updateResponse = await client.PatchAsync(createResponse.Headers.Location, updateContent);

            updateResponse.EnsureSuccessStatusCode();
            var getResponse = await client.GetAsync(createResponse.Headers.Location);
            getResponse.EnsureSuccessStatusCode();
            var newParkingLot = await DeserializeResponseBodyAsync<ParkingLotDto>(getResponse);
            Assert.Equal(lotUpdate.Capacity, newParkingLot.Capacity);
        }

        [Fact]
        public async Task Should_not_update_parking_lot_capacity_when_patch_update_with_negative_capacity()
        {
            var client = GetClient();
            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);
            var createResponse = await client.PostAsync(RootUri, parkingLotContent);
            createResponse.EnsureSuccessStatusCode();

            var lotUpdate = new ParkingLotUpdateDto()
            {
                Capacity = -1,
            };
            var updateContent = SerializeRequestBody(lotUpdate);
            var updateResponse = await client.PatchAsync(createResponse.Headers.Location, updateContent);

            Assert.False(updateResponse.IsSuccessStatusCode);
            var getResponse = await client.GetAsync(createResponse.Headers.Location);
            getResponse.EnsureSuccessStatusCode();
            var newParkingLot = await DeserializeResponseBodyAsync<ParkingLotDto>(getResponse);
            Assert.Equal(parkingLot.Capacity, newParkingLot.Capacity);
        }

        [Fact]
        public async Task Should_delete_parking_lot_when_delete()
        {
            var client = GetClient();
            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);
            var createResponse = await client.PostAsync(RootUri, parkingLotContent);
            createResponse.EnsureSuccessStatusCode();

            var deleteResponse = await client.DeleteAsync(createResponse.Headers.Location);
            deleteResponse.EnsureSuccessStatusCode();

            var getResponse = await client.GetAsync(RootUri);
            getResponse.EnsureSuccessStatusCode();
            var returnedParkingLots = await DeserializeResponseBodyAsync<List<ParkingLotDto>>(getResponse);
            Assert.Empty(returnedParkingLots);
        }

        [Fact]
        public async Task Should_not_delete_parking_lot_when_orders_are_not_all_closed()
        {
            var client = GetClient();
            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);
            var createResponse = await client.PostAsync(RootUri, parkingLotContent);
            createResponse.EnsureSuccessStatusCode();

            var newOrder = SeedOrder();
            var orderContent = SerializeRequestBody(newOrder);
            var createFirstOrderResponse = await client.PostAsync(RootUriForOrders, orderContent);
            createFirstOrderResponse.EnsureSuccessStatusCode();

            var deleteResponse = await client.DeleteAsync(createResponse.Headers.Location);

            var getResponse = await client.GetAsync(RootUri);
            getResponse.EnsureSuccessStatusCode();
            var returnedParkingLots = await DeserializeResponseBodyAsync<List<ParkingLotDto>>(getResponse);
            Assert.Single(returnedParkingLots);
            Assert.Equal(parkingLot, returnedParkingLots[0]);
        }

        private OrderCreateDto SeedOrder()
        {
            return new OrderCreateDto()
            {
                ParkingLotName = "first",
                PlateNumber = "JX123",
            };
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

        private List<ParkingLotDto> SeedParkingLotsForPagingTest()
        {
            var parkinglotsForPaging = new List<ParkingLotDto>();

            var random = new Random();
            for (int i = 0; i < 50; i++)
            {
                parkinglotsForPaging.Add(new ParkingLotDto()
                {
                    Name = Guid.NewGuid().ToString("N"),
                    Capacity = random.Next(1, 100),
                    Location = "everywhere",
                });
            }

            return parkinglotsForPaging;
        }
    }
}
