using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Code
    {
        public int Id { get; set; }
        public string Contenu { get; set; } = null!;
        public string? Titre { get; set; }
    }
}
