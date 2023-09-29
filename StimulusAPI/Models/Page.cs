using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Page
    {
        public Page()
        {
            PageComposants = new HashSet<PageComposant>();
            Progressions = new HashSet<Progression>();
        }

        public int Id { get; set; }
        public int Pts { get; set; }
        public int Ordre { get; set; }
        public int? NoeudId { get; set; }
        public string? Nom { get; set; }

        public virtual Noeud? Noeud { get; set; }
        public virtual ICollection<PageComposant> PageComposants { get; set; }
        public virtual ICollection<Progression> Progressions { get; set; }
    }
}
