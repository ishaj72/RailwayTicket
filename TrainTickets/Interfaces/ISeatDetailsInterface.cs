using TrainTicket.Models;

namespace TrainTicket.Interfaces
{
    public interface ISeatDetailsInterface
    {
        SeatDetails AddSeat(SeatDetails seat);
        SeatDetails UpdateSeat(int seatId, SeatDetails seat);
        bool Delete(int seatId);
    }
}
