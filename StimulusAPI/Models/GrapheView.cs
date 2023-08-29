using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class GrapheView
    {
        public string? Code { get; set; }
        public int Id { get; set; }
        public int LiaisonPrincipal { get; set; }
        public int? CodeStatus { get; set; }
        public int? GrapheId { get; set; }
        public DateTime? Disponibilite { get; set; }
    }
}
