using System.ComponentModel.DataAnnotations;

namespace StimulusFrontEnd.Models
{
    public class Groupe
    {
        [Required]
        [StringLength(9, ErrorMessage = "Le nom du groupe est plus grand que 9 caractère")]
        public string? Nom { get; set; }
        [Required]
        public int? CoursId { get; set; }
        [Required]
        public int? ProfesseurId { get; set; }
    }
}
