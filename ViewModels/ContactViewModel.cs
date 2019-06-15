using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Name is a required field")]
        [MinLength(5, ErrorMessage = "Name must have 5 caracters at least")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail is a required field")]
        [EmailAddress(ErrorMessage = "Type some valid e-mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is a required field")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message a required field")]
        [MaxLength(250, ErrorMessage ="Too long message")]
        public string Message { get; set; }
    }
}
