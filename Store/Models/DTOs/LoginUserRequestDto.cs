using System.ComponentModel.DataAnnotations;

namespace Store.Models.DTOs
{
    public class LoginUserRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
