using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Reference
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public string? Description { get; set; }
    }
}
