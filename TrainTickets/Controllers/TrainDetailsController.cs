using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainDetailsController : ControllerBase
    {
        private readonly ITrainDetailsInterface _trainDetailsInterface;

        public TrainDetailsController(ITrainDetailsInterface trainDetailsInterface)
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

        [HttpPut("UpdateTrain/{trainNumber}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTrain(int trainNumber, [FromBody] TrainDetailsDto trainDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTrain = _trainDetailsInterface.UpdateTrain(trainNumber, trainDto);
            if (updatedTrain != null)
            {
                return Ok(updatedTrain);
            }
            return NotFound("Train not found.");
        }

        [HttpGet("GetTrainByNumber/{trainNumber}")]
        public IActionResult GetTrainByNumber(int trainNumber)
        {
            var train = _trainDetailsInterface.GetTrainByNumber(trainNumber);
            if (train != null)
            {
                return Ok(train);
            }
            return NotFound("Train not found.");
        }

        [HttpDelete("Delete/{trainNumber}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int trainNumber)
        {
            var deleted = _trainDetailsInterface.Delete(trainNumber);
            if (deleted)
            {
                return Ok("Train deleted successfully.");
            }
            return NotFound("Train not found.");
        }
    }
}
