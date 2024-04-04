using System.ComponentModel.DataAnnotations;

namespace TrainTicket.Models
{
    public class UserDetails
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter the name")]
        public string UserName { get; set; }

        public string UserPassword { get; set; }

        [Key]
        [Required(ErrorMessage = "Please enter the emailId")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string UserEmail { get; set; }

        public string UserPhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter the address")]
        public string UserAddress { get; set; }

        [Required(ErrorMessage = "Please enter the age")]
        [Range(18,99,ErrorMessage = "Age should be between 18 to 99" )]
        public int UserAge { get; set; }

        [Required(ErrorMessage = "Please enter the gender")]
        public string UserGender { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
