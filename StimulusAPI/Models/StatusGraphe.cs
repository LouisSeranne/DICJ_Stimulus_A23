using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class StatusGraphe
    {
        public StatusGraphe()
        {
            Graphes = new HashSet<Graphe>();
        }

        public string Code { get; set; } = null!;
        public string Nom { get; set; } = null!;

        public virtual ICollection<Graphe> Graphes { get; set; }
    }
}
