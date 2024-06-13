using System.ComponentModel.DataAnnotations;

namespace ChatMenezes.Domain.Aggregates.UserAgg.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string? Name { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid E-mail")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The passwords don't match")]
        public string? PasswordConfirmation { get; set; }
    }
}
