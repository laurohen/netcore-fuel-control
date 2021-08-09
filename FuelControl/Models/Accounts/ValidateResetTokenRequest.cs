using System.ComponentModel.DataAnnotations;

namespace FuelControl.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}