using System.ComponentModel.DataAnnotations;

namespace SpicyTemplate.Areas.SpicyAdmin.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        [MinLength(4)]
        [MaxLength(25)]
        public string UsernameOrEmail { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
