using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingLotApi.Dtos;
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

        [HttpGet]
        public async Task<ActionResult<List<ParkingLotDto>>> GetAll()
        {
            List<ParkingLotDto> parkingLotDtos = await this.parkingLotService.GetAll();
            return Ok(parkingLotDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetById(int id)
        {
            var parkingLotDto = await this.parkingLotService.GetById(id);
            return Ok(parkingLotDto);
        }

        [HttpGet("{pageIndex}&{pageSize}")]
        public async Task<ActionResult<List<ParkingLotDto>>> GetByPage(int pageIndex, int pageSize = 15)
        {
            var parkingLotDtos = await this.parkingLotService.GetByPage(pageIndex, pageSize);
            return Ok(parkingLotDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            int? id = await parkingLotService.AddParkingLot(parkingLotDto);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, parkingLotDto);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await this.parkingLotService.DeleteParkingLot(id);
            return this.NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateParkingLotCapacity(int id, ParkingLotUpdateDto parkingLotUpdateDto)
        {
            var updatedParkingLot = await parkingLotService.UpdateParkingLotCapacity(id, parkingLotUpdateDto);
            return Ok(updatedParkingLot);
        }
    }
}
