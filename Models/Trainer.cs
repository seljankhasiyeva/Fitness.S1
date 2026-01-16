using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.S1.Models
{
    public class Trainer: Base.BaseEntity
    {
        [MaxLength(15, ErrorMessage ="Name cannot contain more than 20 symbols")]
        public string Name { get; set; }

        [MaxLength(15, ErrorMessage = "Speciality cannot contain more than 20 symbols")]
        public string Specialty { get; set; }

        [MaxLength(500, ErrorMessage = "Bio cannot contain more than 20 symbols")]
        public string Bio { get; set; }
        public string PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
