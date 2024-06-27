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
            seat.SeatStatus = "Not Reserved";
            _context.Seats.Add(seat);
            _context.SaveChanges();
            return seat;
        }

        public SeatDetails UpdateSeat(int seatId, SeatDetails seat)
        {
            var existingSeat = _context.Seats.FirstOrDefault(t => t.SeatId == seatId);

            if (existingSeat != null)
            {
                existingSeat.SeatType = seat.SeatType;
                existingSeat.SeatNumber = seat.SeatNumber;
                existingSeat.SeatStatus = seat.SeatStatus;
                _context.SaveChanges();

                return existingSeat;
            }
            return null;
        }

        public bool Delete(int seatId)
        {
            var seatToDelete = _context.Seats.FirstOrDefault(u => u.SeatId == seatId);

            if (seatToDelete != null)
            {
                _context.Seats.Remove(seatToDelete);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
