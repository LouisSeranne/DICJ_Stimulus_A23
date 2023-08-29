using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Exercice
    {
        public Exercice()
        {
            FichierSauvegardes = new HashSet<FichierSauvegarde>();
            FichierSources = new HashSet<FichierSource>();
        }

        public int Id { get; set; }
        public string? Solution { get; set; }

        public virtual ICollection<FichierSauvegarde> FichierSauvegardes { get; set; }
        public virtual ICollection<FichierSource> FichierSources { get; set; }
    }
}
