using Microsoft.AspNetCore.Mvc;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Repository
{
    public class AdminLoginRepository : IAdminLoginInterface
    {
        private readonly ReservationDbContext _context;

        public AdminLoginRepository(ReservationDbContext context)
        {
            _context = context;
        }

        public Admin AdminLogin([FromQuery] string id, [FromQuery] string name, [FromQuery] string password)
        {
            return _context.Admins.FirstOrDefault(u => u.AdminId == id && u.AdminPassword == password && u.AdminName == name);
        }
    }
}
