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
            //Assert.Equal(companyDto.Employees.Count, returnCompanies[0].Employees.Count);
            //Assert.Equal(companyDto.Employees[0].Age, returnCompanies[0].Employees[0].Age);
            //Assert.Equal(companyDto.Employees[0].Name, returnCompanies[0].Employees[0].Name);
            //Assert.Equal(companyDto.Profile.CertId, returnCompanies[0].Profile.CertId);
            //Assert.Equal(companyDto.Profile.RegisteredCapital, returnCompanies[0].Profile.RegisteredCapital);
        }
    }
}