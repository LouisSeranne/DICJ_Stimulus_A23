using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Progression
    {
        public Progression()
        {
            FichierSauvegardes = new HashSet<FichierSauvegarde>();
        }

        public int PageId { get; set; }
        public string EtudiantDa { get; set; } = null!;
        public int? Pts { get; set; }

        public virtual Etudiant EtudiantDaNavigation { get; set; } = null!;
        public virtual Page Page { get; set; } = null!;
        public virtual ICollection<FichierSauvegarde> FichierSauvegardes { get; set; }
    }
}
