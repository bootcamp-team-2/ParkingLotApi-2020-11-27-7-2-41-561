using System;
using System.Collections.Generic;
using System.Text;
using ParkingLotApi;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    [Collection("ControllerTests")]
    public class OrdersControllerTests : TestBase
    {
        public OrdersControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }
    }
}
