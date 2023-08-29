using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Etudiant
    {
        public Etudiant()
        {
            Progressions = new HashSet<Progression>();
            Groupes = new HashSet<Groupe>();
        }

        public string CodeDa { get; set; } = null!;
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? MotDePasse { get; set; }

        public virtual ICollection<Progression> Progressions { get; set; }

        public virtual ICollection<Groupe> Groupes { get; set; }
    }
}
