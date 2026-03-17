using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]   // its checking the email address is correct or not
        [EmailAddress]
        public string Email { get; set; }
    }
}