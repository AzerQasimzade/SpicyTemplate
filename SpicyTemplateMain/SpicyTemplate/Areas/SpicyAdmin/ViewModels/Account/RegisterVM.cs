using System.ComponentModel.DataAnnotations;

namespace SpicyTemplate.Areas.SpicyAdmin.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Username { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(250)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
