using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Cour
    {
        public Cour()
        {
            Groupes = new HashSet<Groupe>();
        }

        public int Id { get; set; }
        public string CodeCours { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Groupe> Groupes { get; set; }
    }
}
