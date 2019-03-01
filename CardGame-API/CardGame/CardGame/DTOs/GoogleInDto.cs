using System.ComponentModel.DataAnnotations;

namespace CardGame.DTOs
{
    public class GoogleInDto
    {
        [Required]
        public string username;
        [Required]
        public string idToken;
        [Required]
        [EmailAddress]
        public string googleEmail;
    }
}
