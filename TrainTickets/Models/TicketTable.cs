using System.ComponentModel.DataAnnotations;

namespace TrainTicket.Models
{
    public class TicketTable
    { 
        [Key]
        public string PNR {  get; set; }
        [Required]
        public string PassengerName { get; set; }
        [Required]
        public int PassangerAge { get; set; }
        [Required]
        public string PassengerGender { get; set; }
        [Required]
        public string SeatQuota { get; set; }
        [Required]
        public string SeatType { get; set; }
        [Required]
        public int TrainNumber { get; set; }
    }
}
