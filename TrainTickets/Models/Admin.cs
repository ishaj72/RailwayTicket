using System.ComponentModel.DataAnnotations;

namespace TrainTicket.Models
{
    public class Admin
    {
        [Key]
        [Required(ErrorMessage = "Enter the Id")]
        public string AdminId { get; set; }

        [Required(ErrorMessage = "Enter the admin name")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Enter the password")]
        public string AdminPassword { get; set; }
    }
}
