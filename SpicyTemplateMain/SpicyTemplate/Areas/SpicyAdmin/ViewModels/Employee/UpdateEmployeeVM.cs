namespace SpicyTemplate.Areas.SpicyAdmin.ViewModels.Employee
{
    public class UpdateEmployeeVM
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
