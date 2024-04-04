using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginInterface _userLoginInterface;
        private readonly IConfiguration _configuration;
        private DateTime expires;
        private readonly ReservationDbContext _context;

        public UserLoginController(IUserLoginInterface userLoginInterface , IConfiguration configuration, ReservationDbContext context)
        {
            _userLoginInterface = userLoginInterface;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult Login(string userId, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToken = Authenticate(userId, password);

            if (userToken != null)
            {
                var token = Generate(userToken);
                return Ok(token);
            }

            return BadRequest("ID or password is not correct");
        }

        private string Generate(UserDetails userToken)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim (ClaimTypes.Name , userToken.UserName),
                new Claim(ClaimTypes.StreetAddress, userToken.UserAddress),
                new Claim(ClaimTypes.Email , userToken.UserEmail),
                new Claim(ClaimTypes.Gender , userToken.UserGender),
                new Claim("age", userToken.UserAge.ToString(), ClaimValueTypes.Integer)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims,
                expires = DateTime.Now.AddMinutes(15),
                signingCredentials : credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserDetails Authenticate(string userId, string password)
        {
            var checkUser = _userLoginInterface.Login(userId, password);
            if (checkUser != null)
            {
                return checkUser;
            }
            return null;
        }
        [HttpGet("TrainDetails")]
        public List<TrainDetails> GetTrain()
        {
            return _context.Trains.ToList();
        }
    }
}
