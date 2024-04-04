using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatDetailsController : ControllerBase
    {
        private readonly ISeatDetailsInterface _seatDetailsInterface;
        private readonly ITrainDetailsInterface _trainDetailsInterface;
        private readonly IMapper _mapper;

        public SeatDetailsController(ISeatDetailsInterface seatDetailsInterface, ITrainDetailsInterface trainDetailsInterface,IMapper mapper)
        {
            _seatDetailsInterface = seatDetailsInterface;
            _trainDetailsInterface = trainDetailsInterface;
            _mapper = mapper;   
        }

        [HttpPost("AddSeat")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddSeat([FromBody] SeatDetailsDto seatDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var seat = _mapper.Map<SeatDetails>(seatDto);

            var existingTrain = _trainDetailsInterface.GetTrainByNumber(seatDto.TrainNumber);
            if (existingTrain == null)
            {
                return BadRequest("Train number does not exist.");
            }
            var newSeat = _seatDetailsInterface.AddSeat(seat);
            if (newSeat != null)
            {
                var newSeatDto = _mapper.Map<SeatDetailsDto>(newSeat);
                return Ok(newSeatDto);
            }

            return BadRequest("Seat not added.");
        }
        //[HttpPut("UpdateTrain")]
        //[Authorize(Roles = "Admin")]
        //public IActionResult UpdateSeat(int seatId, [FromBody] SeatDetailsDto seatDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var updatedSeat = _seatDetailsInterface.UpdateSeat(seatId, seatDto);
        //    if (updatedSeat != null)
        //    {
        //        return Ok("Seat Details Updated");
        //    }
        //    return NotFound("Seat not found.");
        //}
        //[HttpDelete("Delete")]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Delete(int seatId)
        //{
        //    var deleted = _seatDetailsInterface.Delete(seatId);
        //    if (deleted)
        //    {
        //        return Ok("Seat deleted successfully.");
        //    }
        //    return NotFound("Seat not found.");
        //}
    }
}