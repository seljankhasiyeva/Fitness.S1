using System.ComponentModel.DataAnnotations;

namespace Fitness.S1.ViewModels
{
    public class RegisterVM
    {
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        [MaxLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
