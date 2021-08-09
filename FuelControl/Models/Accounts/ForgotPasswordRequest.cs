using System.ComponentModel.DataAnnotations;

namespace FuelControl.Models.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}