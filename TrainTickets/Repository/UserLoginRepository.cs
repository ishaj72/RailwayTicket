using Microsoft.AspNetCore.Mvc;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Repository
{
    public class UserLoginRepository : IUserLoginInterface
    {
        private readonly ReservationDbContext _context;

        public UserLoginRepository(ReservationDbContext context)
        {
            _context = context;
        }

        public UserDetails Login([FromQuery] string userid, [FromQuery] string password)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userid && u.UserPassword == password);
        }
    }
}
