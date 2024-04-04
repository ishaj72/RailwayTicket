using Microsoft.AspNetCore.Mvc;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ReservationDbContext _context;
        private readonly ISearchDetailsInterface _searchDetailsInterface;

        public SearchController(ReservationDbContext context, ISearchDetailsInterface searchDetailsInterface)
        {
            _context = context;
            _searchDetailsInterface = searchDetailsInterface;
        }

        [HttpPost("Search")]
        public TrainDetails SearchDetails(string source, string destination)
        {
            var checkTrain = _searchDetailsInterface.SearchDetails(source, destination);
            if (checkTrain != null)
            {
                return checkTrain;
            }
            return null;
        }
    }
}
