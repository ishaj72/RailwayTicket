using Microsoft.AspNetCore.Mvc;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;

        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] UserDetails user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newUser = _userInterface.Create(user);
            if (newUser != null)
            {
                return Ok("User registered successfully. Thank you!");
            }
            return BadRequest("Failed to register user.");
        }

        [HttpPut("Update")]
        public IActionResult Update(string emailId, [FromBody] UserDetails updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _userInterface.UpdateUser(emailId, updatedUser);
            if (existingUser != null)
            {
                return Ok("User details updated successfully.");
            }
            return NotFound("User not found.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string emailId)
        {
            var deleted = _userInterface.Delete(emailId);
            if (deleted)
            {
                return Ok("User deleted successfully.");
            }
            return NotFound("User not found.");
        }

        [HttpPost("BookTickets")]
        public IActionResult BookTickets([FromBody] List<TicketTable> tickets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookedTicketsWithPrice = _userInterface.BookTickets(tickets);
            if (bookedTicketsWithPrice.Count > 0)
            {
                // Construct a response containing the success message and ticket prices
                var response = new
                {
                    Message = "Tickets booked successfully. Thank you!",
                    Tickets = bookedTicketsWithPrice.Select(bt => new
                    {
                        Ticket = bt.Item1,
                        Price = bt.Item2
                    })
                };
                return Ok(response);
            }
            return BadRequest("Failed to book tickets.");
        }



        [HttpDelete("CancelTicket")]
        public IActionResult CancelTicket(string pnr)
        {
            var canceledTicket = _userInterface.CancelTicket(pnr);

            if (canceledTicket != null)
            {
                return Ok("Ticket canceled successfully.");
            }

            return NotFound("Ticket not found.");
        }


        [HttpPost("ChangePassword")]
        public IActionResult ResetPassword(string userEmail, string newPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newpass = _userInterface.ResetPassword(userEmail, newPassword);
            if (newpass != null)
            {
                return Ok("Your password is changed");
            }
            return BadRequest("Password Changed");
        }
    }
}