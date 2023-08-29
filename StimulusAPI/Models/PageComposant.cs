using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class PageComposant
    {
        public int Id { get; set; }
        public int Ordre { get; set; }
        public string? TypeComposant { get; set; }
        public int IdReference { get; set; }
        public int? PageId { get; set; }

        public virtual Page? Page { get; set; }
    }
}
