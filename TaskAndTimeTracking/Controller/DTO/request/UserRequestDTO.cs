using System.ComponentModel.DataAnnotations;

namespace TaskAndTimeTracking.Controller.DTO
{
    public class UserRequestDTO : BaseDTO
    {
        public UserRequestDTO()
        {
        }

        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Length of the firstname must be between 2 and 50 characters")]
        public string FirstName { get; set; }
        
        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Length of the lastname must be between 2 and 50 characters")]
        public string LastName { get; set; }
        
        [Required, EmailAddress(ErrorMessage = "Invalid E-Mail Address")]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Range(1, 10, ErrorMessage = "Range must be between 1  and 10")]
        public int AuthorizationLevel { get; set; }
        
        public bool PasswordModified { get; set; }
    }
}