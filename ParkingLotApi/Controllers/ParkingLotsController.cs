using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {
        private readonly IParkingLotService parkingLotService;
        public ParkingLotsController(IParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> AddAsync(ParkingLotDto parkingLotDto)
        {
            var createdParkingLotId = await this.parkingLotService.AddAsync(parkingLotDto);
            if (string.IsNullOrEmpty(createdParkingLotId))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetAsync), new { id = createdParkingLotId }, parkingLotDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingLotDto>>> GetAllAsync(int? limit, int? offset)
        {
            var searchedParkingLots = await this.parkingLotService.GetAllAsync(limit, offset);
            return Ok(searchedParkingLots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetAsync(string id)
        {
            var searchedParkingLot = await this.parkingLotService.GetAsync(id);

            if (searchedParkingLot == null)
            {
                return NotFound();
            }

            return Ok(searchedParkingLot);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAsync(string id, ParkingLotUpdateDto parkingLotUpdateDto)
        {
            if (parkingLotUpdateDto.Capacity < 0)
            {
                return BadRequest();
            }

            var parkingLotToUpdate = await this.parkingLotService.GetAsync(id);
            if (parkingLotToUpdate == null)
            {
                return NotFound();
            }

            await this.parkingLotService.Update(id, parkingLotUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var parkingLotToUpdate = await this.parkingLotService.GetAsync(id);
            if (parkingLotToUpdate == null)
            {
                return NotFound();
            }

            await this.parkingLotService.Delete(id);
            return NoContent();
        }
    }

    public class PagingOption
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}