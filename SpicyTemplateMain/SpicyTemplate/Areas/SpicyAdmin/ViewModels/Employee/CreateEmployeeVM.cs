using System.ComponentModel.DataAnnotations;

namespace SpicyTemplate.Areas.SpicyAdmin.ViewModels.Employee
{
    public class CreateEmployeeVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
