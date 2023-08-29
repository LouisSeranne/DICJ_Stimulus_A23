namespace StimulusFrontEnd.Models
{
    public class Video : TheorieComponent
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
    }
}
