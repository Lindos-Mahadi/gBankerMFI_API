using System.ComponentModel.DataAnnotations;

namespace GC.MFI.Security.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
