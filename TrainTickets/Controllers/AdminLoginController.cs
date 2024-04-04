using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        public readonly IAdminLoginInterface _adminLoginInterface;
        private readonly IConfiguration _configuration;
        private DateTime expires;
        private readonly ReservationDbContext _context;

        public AdminLoginController(IAdminLoginInterface adminLoginInterface, IConfiguration configuration, ReservationDbContext context)
        {
            _adminLoginInterface = adminLoginInterface;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Admin")]
        public IActionResult AdminLogin(string id, string name, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToken2 = Authenticate(id, name, password);

            if (userToken2 != null)
            {
                var token = Generate(userToken2);
                return Ok(token);
            }

            return BadRequest("ID or password is not correct");
        }

        private string Generate(Admin userToken2)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
            new Claim("Id", userToken2.AdminId.ToString(), ClaimValueTypes.Integer),
            new Claim("Password", userToken2.AdminPassword.ToString(), ClaimValueTypes.Integer),
            new Claim(ClaimTypes.Name, userToken2.AdminName)
            };
            var isAdmin = _context.Admins.Any(admin => admin.AdminId == userToken2.AdminId);
            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Admin Authenticate(string id, string name, string password)
        {
            var checkAdmin = _adminLoginInterface.AdminLogin(id, name, password);
            if (checkAdmin != null)
            {
                return checkAdmin;
            }
            return null;
        }

        [HttpGet("TrainDetails")]
        public List<TrainDetails> GetTrain()
        {
            return _context.Trains.ToList();
        }
        [HttpGet("SeatDetails")]
        public List<SeatDetails> GetSeat()
        {
            return _context.Seats.ToList();
        }
    }
}
