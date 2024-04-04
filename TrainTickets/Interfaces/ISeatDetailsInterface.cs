using TrainTicket.Models;

namespace TrainTicket.Interfaces
{
    public interface ISeatDetailsInterface
    {
        SeatDetails AddSeat(SeatDetails seat);
        //SeatDetailsDto UpdateSeat(int seatId, SeatDetailsDto seatDto);
        //bool Delete(int seatId);
    }
}
