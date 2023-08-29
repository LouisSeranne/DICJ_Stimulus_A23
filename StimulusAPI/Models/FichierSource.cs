using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class FichierSource
    {
        public int Id { get; set; }
        public string Contenu { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public int? ExerciceId { get; set; }

        public virtual Exercice? Exercice { get; set; }
    }
}
