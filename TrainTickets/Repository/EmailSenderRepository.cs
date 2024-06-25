using System.Net;
using System.Net.Mail;
using System.Text;
using TrainTicket.Interfaces;
using TrainTicket.Models;

namespace TrainTicket.Repository
{
    public class EmailSenderRepository : IEmailSenderInterface
    {
        public void SendEmail(string toEmail, string subject)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("railways.ticket22@gmail.com", "mlwt sgqr bsaz ppbx");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("railways.ticket22@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>User Registered!!</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>Thank you for registering with our online ticket reservation Site.This mesage confirms that your account has been created.</p>");
            mailBody.AppendFormat("<br />");
            mailMessage.Body = mailBody.ToString();
            client.Send(mailMessage);
        }

        public void SendEmailForTicket(string recipientEmail, string subject, TrainDetails trainDetails, SeatDetails seatDetails, TicketTable ticket)
        {
            string body = $"Dear User,\n\nYour ticket has been successfully booked!\n\n" +
                            $"Train Name: {trainDetails.TrainName}\n" +
                            $"Source Arrival: {trainDetails.SourceArrival}\n" +
                            $"Source Departure: {trainDetails.SourceDeparture}\n" +
                            $"Seat Type: {seatDetails.SeatType}\n" +
                            $"Seat Number: {seatDetails.SeatNumber}\n" +
                            $"PNR: {ticket.PNR}\n\n" +
                            $"Thank you for choosing our service!\n\nBest Regards,\nTrainTicket Team";

            SendEmailA(recipientEmail, subject, body); // Passing body parameter to SendEmail method
        }

        public void SendEmailA(string toEmail, string subject, string body)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("railways.ticket22@gmail.com", "mlwt sgqr bsaz ppbx");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("railways.ticket22@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body; // Set the message body here
            client.Send(mailMessage);
        }

    }
}
