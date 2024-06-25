using TrainTicket.Models;

namespace TrainTicket.Interfaces
{
    public interface IUserInterface
    {
        UserDetails Create(UserDetails user);
        UserDetails UpdateUser(string emailId, UserDetails updatedUser);
        UserDetails ResetPassword(string userEmail, string newPassword);
        bool Delete(string emailId);
        List<(TicketTable, decimal)> BookTickets(List<TicketTable> tickets);
        TicketTable CancelTicket(string pnr);
    }
}
