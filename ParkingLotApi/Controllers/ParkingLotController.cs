using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Service;

namespace ParkingLotApi.Controllers
{
    [Route("ParkingLotsApi")]
    [ApiController]
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;

        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpPost("parkingLots")]
        public async Task<(ActionResult<ParkingLotDto>, string message)> Add(ParkingLotDto parkingLotDto)
        {
            if (string.IsNullOrEmpty(parkingLotDto.Name))
            {
                string message = "name of parkingLot can not be null or empty";
                return (BadRequest(null), message);
            }

            var name = await this.parkingLotService.AddParkingLotAsync(parkingLotDto);

            return (CreatedAtAction(nameof(GetByName), new { Name = name }, parkingLotDto), null);
        }

        [HttpGet("parkingLots/{name}")]
        public async Task<ActionResult<ParkingLotDto>> GetByName(string name)
        {
            var parkingLotDto = await this.parkingLotService.GetByNameAsync(name);
            return Ok(parkingLotDto);
        }
    }
}
