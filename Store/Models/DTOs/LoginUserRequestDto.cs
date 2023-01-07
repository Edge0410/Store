using System.ComponentModel.DataAnnotations;

namespace Store.Models.DTOs
{
    public class LoginUserRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
