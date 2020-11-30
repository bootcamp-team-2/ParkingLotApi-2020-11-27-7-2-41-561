using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ParkingLotApi;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    public class OrderControllerTest : TestBase
    {
        public OrderControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Create_New_Order_Success()
        {
        }
    }
}
