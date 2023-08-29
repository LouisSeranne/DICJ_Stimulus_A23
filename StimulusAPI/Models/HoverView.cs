using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class HoverView
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? PtsTotal { get; set; }
        public string? CodeParent { get; set; }
        public int? GrapheId { get; set; }
        public DateTime? Disponibilite { get; set; }
    }
}
