using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrainTicket.Models
{
    public class SeatDetailsDto
    {
        [Required]
        [Range(1, 70, ErrorMessage = "Range should be between 1 to 70")]
        public int SeatNumber { get; set; }

        [Required]
        [RegularExpression("^(General|Sleeper|1AC|2AC|3AC)$", ErrorMessage = "Seat type should be general, sleeper,1AC,2AC,3AC")]
        public string SeatType { get; set; }

        [Required]
        public string SeatStatus { get; set; } = "Not Reserved";

        [Required]
        [ForeignKey("Train")]
        public int TrainNumber { get; set; }
    }
}
