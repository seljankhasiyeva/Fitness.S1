namespace Fitness.S1.Areas.Admin.ViewModels
{
    public class UpdateTrainerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
