using System.ComponentModel.DataAnnotations;

namespace TrainTicket.Models
{
    public class TrainDetails
    {
        [Required]
        public string TrainName { get; set; }
        [Key]
        [Range(100000, 999999)]
        public int TrainId { get; set; }
        [Required]
        public int TrainNumber { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime SourceArrival { get; set; }
        [Required]
        public DateTime SourceDeparture { get; set; }
        [Required]
        public DateTime DestinationArrival { get; set; }
        [Required]
        public DateTime DestinationDeparture { get; set; }
    }
}
