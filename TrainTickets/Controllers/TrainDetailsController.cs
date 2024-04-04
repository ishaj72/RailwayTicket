using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainDetailsController : ControllerBase
    {
        private readonly ITrainDetailsInterface _trainDetailsInterface;
        private readonly ReservationDbContext _context;

        public TrainDetailsController(ITrainDetailsInterface trainDetailsInterface,ReservationDbContext context)
        {
            _trainDetailsInterface = trainDetailsInterface;
        }

        [HttpPost("AddTrain")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddTrain([FromBody] TrainDetailsDto trainDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime minDepartureDate = DateTime.Now; 
            DateTime maxDepartureDate = DateTime.Now.AddMonths(3); 

            if (trainDto.SourceDeparture >= minDepartureDate &&
                trainDto.DestinationDeparture >= minDepartureDate &&
                trainDto.SourceArrival >= minDepartureDate &&
                trainDto.DestinationArrival >= minDepartureDate &&
                trainDto.SourceDeparture <= maxDepartureDate &&
                trainDto.DestinationDeparture <= maxDepartureDate &&
                trainDto.SourceArrival <= maxDepartureDate &&
                trainDto.DestinationArrival <= maxDepartureDate)
            {
                var newTrain = _trainDetailsInterface.AddTrains(trainDto);
                if (newTrain != null)
                {
                    return Ok(newTrain);
                }
                return BadRequest("Train could not be added");
            }
            return BadRequest("Train departure or arrival times are not within the valid range");
        }


        [HttpPut("UpdateTrain")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTrain(int trainid,[FromBody] TrainDetailsDto trainDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTrain = _trainDetailsInterface.UpdateTrain(trainid,trainDto);
            if (updatedTrain != null)
            {
                return Ok(updatedTrain);
            }
            return NotFound("Train not found.");
        }
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int trainid)
        {
            var deleted = _trainDetailsInterface.Delete(trainid);
            if (deleted)
            {
                return Ok("Train deleted successfully.");
            }
            return NotFound("Train not found.");
        }
    }
}