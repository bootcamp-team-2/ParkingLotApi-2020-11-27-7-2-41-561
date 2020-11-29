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
    [Route("[controller]")]
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;

        public ParkingLotsController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add(ParkingLotDto parkingLotDto)
        {
            string errorMessage = string.Empty;
            if (!parkingLotDto.IsValid(out errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var id = await this.parkingLotService.AddParkingLot(parkingLotDto);

            return CreatedAtAction(nameof(GetById), new { id = id }, parkingLotDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetById(int id)
        {
            var parkingLotDto = await this.parkingLotService.GetById(id);
            return Ok(parkingLotDto);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<int>> DeleteParkingLotAsync(string name)
        {
            if (!await parkingLotService.ContainExistingParkingLot(name))
            {
                return NotFound();
            }

            await parkingLotService.DeleteParkingLot(name);
            return NoContent();
        }
    }
}
