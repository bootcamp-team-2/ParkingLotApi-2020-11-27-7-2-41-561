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
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;

        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpPost("parkingLots")]
        public async Task<ActionResult<int>> Add(ParkingLotDto parkingLotDto)
        {
            string errorMessage = string.Empty;
            if (!parkingLotDto.IsValid(out errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var name = await this.parkingLotService.AddParkingLot(parkingLotDto);

            return CreatedAtAction(nameof(GetByName), new { Name = name }, parkingLotDto);
        }

        [HttpGet("parkingLots/{name}")]
        public async Task<ActionResult<ParkingLotDto>> GetByName(int id)
        {
            var parkingLotDto = await this.parkingLotService.GetById(id);
            return Ok(parkingLotDto);
        }

        //[HttpDelete("parkingLots/{id}")]
        //public async void Delete(int id)
        //{
        //    await this.parkingLotService.DeleteParkingLot(id);
        //    return NoContent();
        //}
    }
}
