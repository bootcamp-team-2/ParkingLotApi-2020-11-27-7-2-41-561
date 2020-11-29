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

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ParkingLotDto>>> GetALL()
        //{
        //    var parkingLotDtos = await this.parkingLotService.GetAll();
        //    return Ok(parkingLotDtos);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingLotDto>>> GetALL(int?pageSize, int?startPage)
        {
            if (pageSize.HasValue && startPage.HasValue)
            {
                var parkingLotDtos = await this.parkingLotService.GetOnPage(pageSize.Value, startPage.Value);
                return Ok(parkingLotDtos);
            }

            var parkingLotDtosAll = await this.parkingLotService.GetAll();
            return Ok(parkingLotDtosAll);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Add(ParkingLotDto parkingLotDto)
        {
            var isNameExisted = await parkingLotService.IsParkingLotNameExisted(parkingLotDto);
            if (isNameExisted)
            {
                return Conflict(new Dictionary<string, string>() { { "message", "Name of parkingLot exists!" } });
            }

            if (string.IsNullOrEmpty(parkingLotDto.Name) || string.IsNullOrEmpty(parkingLotDto.Location) || parkingLotDto.Capacity < 0)
            {
                return BadRequest(new Dictionary<string, string>() { { "message", "Name and Location of parkingLot can not be null!" } });
            }

            var id = await this.parkingLotService.AddParkingLot(parkingLotDto);

            return CreatedAtAction(nameof(GetById), new { id = id }, parkingLotDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await parkingLotService.DeleteParkingLot(id);

            return this.NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ParkingLotDto>> Update(int id, UpdateParkingLotDto updateParkingLotDto)
        {
            var parkinglot = await this.parkingLotService.UpdateParkingLot(id, updateParkingLotDto.Capacity);

            return Ok(parkinglot);
        }
    }
}
