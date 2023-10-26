using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Administrateur
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? MotDePasse { get; set; }
    }
}
