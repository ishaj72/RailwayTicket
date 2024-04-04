using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainTicket.Models
{
    public class SeatDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }

        [Required]
        [Range(1,70, ErrorMessage = "Range should be between 1 to 70")]
        public int SeatNumber { get; set; }

        [Required]
        public string SeatType { get; set; }

        [Required]
        public string SeatStatus { get; set; } 

        [Required]
        [ForeignKey("Train")]
        public int TrainNumber { get; set; }
    }
}
