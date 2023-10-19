using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Noeud
    {
        public Noeud()
        {
            InverseLiaisonPrincipalNavigation = new HashSet<Noeud>();
            Pages = new HashSet<Page>();
            NoeudEnfants = new HashSet<Noeud>();
            NoeudParents = new HashSet<Noeud>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Disponibilite { get; set; }
        public int? Pts { get; set; }
        public string? Image { get; set; }
        public int? GrapheId { get; set; }
        public int? LiaisonPrincipal { get; set; }
        public bool Obligatoire { get; set; }
        public int? Status { get; set; }
        public decimal? PosX { get; set; }
        public decimal? PosY { get; set; }
        public decimal? Rayon { get; set; }

        public virtual Graphe? Graphe { get; set; }
        public virtual Noeud? LiaisonPrincipalNavigation { get; set; }
        public virtual ICollection<Noeud> InverseLiaisonPrincipalNavigation { get; set; }
        public virtual ICollection<Page> Pages { get; set; }

        public virtual ICollection<Noeud> NoeudEnfants { get; set; }
        public virtual ICollection<Noeud> NoeudParents { get; set; }
    }
}
