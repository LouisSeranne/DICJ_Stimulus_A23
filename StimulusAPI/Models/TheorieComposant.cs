using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class TheorieComposant
    {
        public int Id { get; set; }
        public int Ordre { get; set; }
        public string? Type { get; set; }
        public string? Texte { get; set; }
        public int? NoeudId { get; set; }
    }
}
