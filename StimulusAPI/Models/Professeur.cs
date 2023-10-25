using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Professeur
    {
        public Professeur()
        {
            Groupes = new HashSet<Groupe>();
        }

        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? MotDePasse { get; set; }
        public string? NumEmploye { get; set; }

        public virtual ICollection<Groupe> Groupes { get; set; }
    }
}
