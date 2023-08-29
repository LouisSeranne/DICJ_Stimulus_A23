using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class StatusProgression
    {
        public int Id { get; set; }
        public int? ProgressionNoeudId { get; set; }
        public string? ProgressionEtudiantDa { get; set; }
        public DateTime? Date { get; set; }
        public string? CodeStatus { get; set; }
    }
}
