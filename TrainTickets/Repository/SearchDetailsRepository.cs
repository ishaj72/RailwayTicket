using Microsoft.AspNetCore.Mvc;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Repository
{
    public class SearchDetailsRepository :ISearchDetailsInterface
    {
        private ReservationDbContext _context;
        public SearchDetailsRepository(ReservationDbContext context)
        {
            _context = context;
        }
        public TrainDetails SearchDetails(string source, string destination)
        {
            return _context.Trains.FirstOrDefault(u => u.Source == source && u.Destination == destination);
        }
    }
}
