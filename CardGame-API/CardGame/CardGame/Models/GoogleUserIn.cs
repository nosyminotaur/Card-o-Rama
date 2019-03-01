using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.Models
{
    public class GoogleUserIn
    {
        [EmailAddress]
        [Required]
        public string googleEmail;
        [Required]
        public string idToken;
        [Required]
        public string username;
    }
}
