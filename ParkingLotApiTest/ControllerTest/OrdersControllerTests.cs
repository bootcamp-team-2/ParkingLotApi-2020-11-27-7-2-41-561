using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using Xunit;
using static ParkingLotApiTest.TestTool;

namespace ParkingLotApiTest.ControllerTest
{
    [Collection("ControllerTests")]
    public class OrdersControllerTests : TestBase
    {
        private const string RootUri = "api/orders";
        private const string RootUriForParkingLots = "api/parkinglots";
        public OrdersControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_new_order_when_add_new()
        {
            var client = GetClient();

            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);
            await client.PostAsync(RootUriForParkingLots, parkingLotContent);

            var newOrder = SeedOrder();
            var orderContent = SerializeRequestBody(newOrder);

            var createResponse = await client.PostAsync(RootUri, orderContent);

            createResponse.EnsureSuccessStatusCode();
            var returnedOrder = await DeserializeResponseBodyAsync<OrderDto>(createResponse);
            Assert.Equal(newOrder.PlateNumber, returnedOrder.PlateNumber);
            Assert.Equal(newOrder.ParkingLotName, returnedOrder.ParkingLotName);
        }

        [Fact]
        public async Task Should_not_create_new_order_when_parking_lot_is_full()
        {
            var client = GetClient();

            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);
            var createParkingLotResponse = await client.PostAsync(RootUriForParkingLots, parkingLotContent);

            var firstOrder = SeedOrder();
            var firstOrderContent = SerializeRequestBody(firstOrder);
            var createFirstOrderResponse = await client.PostAsync(RootUri, firstOrderContent);
            createFirstOrderResponse.EnsureSuccessStatusCode();

            var newOrder = SeedOrder();
            newOrder.PlateNumber = "XJ123";
            var orderContent = SerializeRequestBody(newOrder);

            var newCreateResponse = await client.PostAsync(RootUri, orderContent);

            Assert.False(newCreateResponse.IsSuccessStatusCode);

            var getResponse = await client.GetAsync($"{RootUri}/dev");
            getResponse.EnsureSuccessStatusCode();
            var allOrders = await DeserializeResponseBodyAsync<List<OrderEntity>>(getResponse);
            Assert.Single(allOrders);
            Assert.Equal(firstOrder.PlateNumber, allOrders[0].PlateNumber);
        }

        [Fact]
        public async Task Should_update_order_status_when_car_leaves()
        {
            var client = GetClient();

            var parkingLot = SeedParkingLot();
            var parkingLotContent = SerializeRequestBody(parkingLot);
            await client.PostAsync(RootUriForParkingLots, parkingLotContent);

            var newOrder = SeedOrder();
            var orderContent = SerializeRequestBody(newOrder);
            var createResponse = await client.PostAsync(RootUri, orderContent);
            var createdOrder = await DeserializeResponseBodyAsync<OrderDto>(createResponse);
            createResponse.EnsureSuccessStatusCode();

            var orderUpdate = new OrderUpdateDto();
            var updateResponse = await client.PatchAsync(createResponse.Headers.Location, SerializeRequestBody(orderUpdate));

            updateResponse.EnsureSuccessStatusCode();
            var getResponse = await client.GetAsync($"{RootUri}/dev/{createdOrder.OrderNumber}");
            getResponse.EnsureSuccessStatusCode();
            var updatedOrder = await DeserializeResponseBodyAsync<OrderEntity>(getResponse);
            Assert.Equal(orderUpdate.Status, updatedOrder.Status);
            Assert.True(updatedOrder.CloseTimeOffset.HasValue);
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
                Capacity = 1,
                Location = "here",
            };
        }
    }
}
