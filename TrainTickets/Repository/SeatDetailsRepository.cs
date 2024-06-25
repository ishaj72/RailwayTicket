using AutoMapper;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Repository
{
    public class SeatDetailsRepository : ISeatDetailsInterface
    {
        private readonly ReservationDbContext _context;
        private readonly IMapper _mapper;

        public SeatDetailsRepository(ReservationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SeatDetails AddSeat(SeatDetails seat)
        {
            var seatValidation = _context.Seats.FirstOrDefault(s => s.SeatType == "1AC" || s.SeatType == "Sleeper" || s.SeatType == "2AC" || s.SeatType == "3AC" || s.SeatType == "General");
            if (seatValidation != null)
            {
                seat.SeatStatus = "Not Reserved";
                _context.Seats.Add(seat);
                _context.SaveChanges();
                return seat;
            }
            return null;
        }
    }
}

//public SeatDetailsDto UpdateSeat(int seatId, SeatDetailsDto seatDto)
//{
//    var seat = _mapper.Map<SeatDetails>(seatDto);
//    var existingSeat = _context.Seats.FirstOrDefault(t => t.SeatId == seatId);

//    if (existingSeat != null)
//    {
//        existingSeat.SeatType = seat.SeatType;
//        existingSeat.SeatNumber = seat.SeatNumber;
//        _context.SaveChanges();

//        return _mapper.Map<SeatDetailsDto>(existingSeat);
//    }
//    return null;
//}
//public bool Delete(int seatId)
//{
//    var seatToDelete = _context.Seats.FirstOrDefault(u => u.SeatId == seatId);

//    if (seatToDelete != null)
//    {
//        _context.Seats.Remove(seatToDelete);
//        _context.SaveChanges();
//        return true;
//    }

//    return false;
//}