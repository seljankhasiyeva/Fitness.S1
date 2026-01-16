namespace Fitness.S1.Areas.Admin.ViewModels
{
    public class CreateTrainerVM
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Bio { get; set; }
        public IFormFile Photo { get; set; }
    }
}
