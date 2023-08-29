using System;
using System.Collections.Generic;

namespace StimulusAPI.Models
{
    public partial class Video
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public int Longeur { get; set; }
        public int Largeur { get; set; }
        public string? Description { get; set; }
    }
}
