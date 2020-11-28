using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("parkingLots")]
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;

        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetById(int id)
        {
            var parkingLotDto = await this.parkingLotService.GetById(id);
            return Ok(parkingLotDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingLotDto>>> GetALL()
        {
            var parkingLotDtos = await this.parkingLotService.GetAll();
            return Ok(parkingLotDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Add(ParkingLotDto parkingLotDto)
        {
            var isNameExisted = await parkingLotService.IsParkingLotNameExisted(parkingLotDto);
            if (isNameExisted)
            {
                return Conflict(new Dictionary<string, string>() { { "message", "Name of parkingLot exists!" } });
            }

            if (parkingLotDto.Name == null || parkingLotDto.Location == null)
            {
                return BadRequest(new Dictionary<string, string>() { { "message", "Name and Location of parkingLot can not be null!" } });
            }

            var id = await this.parkingLotService.AddParkingLot(parkingLotDto);

            return CreatedAtAction(nameof(GetById), new { id = id }, parkingLotDto);
        }
    }
}
