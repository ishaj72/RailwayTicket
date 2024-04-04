using TrainTicket.Models;

namespace TrainTicket.Interfaces
{
    public interface IUserInterface
    {
        UserDetails Create(UserDetails user);
        UserDetails UpdateUser(string emailId, UserDetails updatedUser);
        UserDetails ResetPassword(string userEmail, string newPassword);
        bool Delete(string emailId);
        TicketTable BookTicket(TicketTable ticket);
        TicketTable CancelTicket(string pnr);

    }
}
