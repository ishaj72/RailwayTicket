using TrainTicket.Interfaces;
using TrainTicket.Models;
using Microsoft.AspNetCore.Authorization;

namespace TrainTicket.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly ReservationDbContext _context;
        private readonly IEmailSenderInterface _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(ReservationDbContext context, IEmailSenderInterface emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize(Roles = "User")]
        public UserDetails Create(UserDetails user)
        {
            int val = user.UserEmail.IndexOf("@");
            user.UserId = val != -1 ? user.UserEmail.Substring(0, val) : "Incorrect email id";

            const string useChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@!#$%";
            Random random = new Random();
            int passwordLength = random.Next(6, 9);
            char[] chars = new char[passwordLength];
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = useChars[random.Next(useChars.Length)];
            }
            user.UserPassword = new string(chars);

            _context.Add(user);
            _context.SaveChanges();
            _emailSender.SendEmail(user.UserEmail, "User Regitered");
            return user;
        }
        [Authorize(Roles = "User")]
        public UserDetails UpdateUser(string emailId, UserDetails updatedUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserEmail == emailId);

            if (existingUser != null)
            {
                
                existingUser.UserAge = updatedUser.UserAge;
                existingUser.UserPhoneNumber = updatedUser.UserPhoneNumber;
                existingUser.UserAddress = updatedUser.UserAddress;

                _context.SaveChanges();
                return existingUser;
            }

            return null;
        }
        [Authorize(Roles = "User")]
        public bool Delete(string emailId)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.UserEmail == emailId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
                return true;
            }

            return false; 
        }

        [Authorize(Roles = "User")]
        public TicketTable BookTicket(TicketTable ticket)
        {
            const string useChars = "1234567890";
            Random random = new Random();
            int pnrLength = random.Next(7,8);
            char[] chars = new char[pnrLength];
            for (int i = 0; i < pnrLength; i++)
            {
                chars[i] = useChars[random.Next(useChars.Length)];
            }
            ticket.PNR = new string(chars);

            var seat = _context.Seats.FirstOrDefault(s => s.SeatType == ticket.SeatType && s.SeatStatus == "Not Reserved");
            var train = _context.Trains.FirstOrDefault(t => t.TrainNumber == ticket.TrainNumber);

            if (seat != null && train != null)
            {
                seat.SeatStatus = "Reserved";
                _context.Add(ticket);
                _context.SaveChanges();

                var userEmail = _httpContextAccessor.HttpContext.User.Identity.Name;
                var trainDetails = new TrainDetails
                {
                    TrainName = train.TrainName,
                    SourceArrival = train.SourceArrival,
                    SourceDeparture = train.SourceDeparture 
                };

                var seatDetails = new SeatDetails
                {
                    SeatType = seat.SeatType,
                    SeatNumber = seat.SeatNumber
                };

                _emailSender.SendEmailForTicket(userEmail, "Ticket Details", trainDetails, seatDetails, ticket);
                return ticket;
            }

            return null;
        }

        public TicketTable CancelTicket(string pnr)
        {
            var ticketToCancel = _context.TicketTables.FirstOrDefault(t => t.PNR == pnr);

            if (ticketToCancel != null)
            {
                var seat = _context.Seats.FirstOrDefault(s => s.SeatType == ticketToCancel.SeatType && s.SeatStatus == "Reserved");

                if (seat != null)
                {
                    seat.SeatStatus = "Not Reserved";
                }

                _context.TicketTables.Remove(ticketToCancel);
                _context.SaveChanges();
                return ticketToCancel;
            }

            return null;
        }


        [Authorize(Roles ="User")]
        public UserDetails ResetPassword(string userEmail, string newPassword)
        {
            var changePassword = _context.Users.FirstOrDefault(u => u.UserEmail == userEmail);
            if (changePassword != null)
            {
                changePassword.UserPassword = newPassword;
                _context.SaveChanges();
            }
            return null;
        }
    }
}
