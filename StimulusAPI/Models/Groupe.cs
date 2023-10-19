using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Groupe
    {
        public Groupe()
        {
            Graphes = new HashSet<Graphe>();
            EtudiantDa = new HashSet<Etudiant>();
        }

        public int Id { get; set; }
        public string? Nom { get; set; }
        public int? CoursId { get; set; }
        public int? ProfesseurId { get; set; }

        public virtual Cour? Cours { get; set; }
        public virtual Professeur? Professeur { get; set; }
        public virtual ICollection<Graphe> Graphes { get; set; }

        public virtual ICollection<Etudiant> EtudiantDa { get; set; }
        public virtual ICollection<Professeur> ProfId { get; set; }
    }
}
