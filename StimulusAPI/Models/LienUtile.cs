using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class LienUtile
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? NomUrl { get; set; }
        public int? GrapheId { get; set; }

        public virtual Graphe? Graphe { get; set; }
    }
}
