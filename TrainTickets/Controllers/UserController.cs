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

        [HttpPost("BookTicket")]
        public IActionResult BookTicket([FromBody] TicketTable ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var newUser = _userInterface.BookTicket(ticket);
            if (newUser != null)
            {
                return Ok("Ticket booked successfully. Thank you!");
            }
            return BadRequest("Failed to book ticket.");
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


        [HttpPost("Change Password")]
        public IActionResult ResetPassword(string userEmail, string newPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newpass = _userInterface.ResetPassword(userEmail, newPassword); 
            if(newpass != null) 
            {
                return Ok("Your password is changed");
            }
            return BadRequest("Password Changed");
        }
    }}
