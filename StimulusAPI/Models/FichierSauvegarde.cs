using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class FichierSauvegarde
    {
        public int Id { get; set; }
        public int? ProgressionPageId { get; set; }
        public string? FichierEtudiantDa { get; set; }
        public string? Contenu { get; set; }
        public int? ExerciceId { get; set; }
        public DateTime? Version { get; set; }
        public string Nom { get; set; } = null!;

        public virtual Exercice? Exercice { get; set; }
        public virtual Progression? Progression { get; set; }
    }
}
