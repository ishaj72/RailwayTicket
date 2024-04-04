using TrainTicket.Models;

namespace TrainTicket.Interfaces
{
    public interface IEmailSenderInterface
    {
        void SendEmail(string toEmail, string subject);
        void SendEmailForTicket(string recipientEmail, string subject, TrainDetails trainDetails, SeatDetails seatDetails, TicketTable ticket);
        void SendEmailA(string toEmail, string subject, string body);
    }
}
