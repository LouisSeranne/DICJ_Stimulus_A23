using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Graphe
    {
        public Graphe()
        {
            LienUtiles = new HashSet<LienUtile>();
            Noeuds = new HashSet<Noeud>();
        }

        public int Id { get; set; }
        public string? Couleur { get; set; }
        public string Nom { get; set; } = null!;
        public int? GroupeId { get; set; }
        public string? StatusCode { get; set; }

        public virtual Groupe? Groupe { get; set; }
        public virtual StatusGraphe? StatusCodeNavigation { get; set; }
        public virtual ICollection<LienUtile> LienUtiles { get; set; }
        public virtual ICollection<Noeud> Noeuds { get; set; }
    }
}
