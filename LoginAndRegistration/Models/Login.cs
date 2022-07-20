using System.ComponentModel.DataAnnotations;

namespace LoginAndRegistration.Models
{
    public class Login
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
