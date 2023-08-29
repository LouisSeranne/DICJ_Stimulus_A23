namespace StimulusFrontEnd.Models
{
    public abstract class TheorieComponent
    {
        public Type TypeComponent { get; set; }
        public Dictionary<string, object>? Parameters { get; set; }
        public int PageId { get; set; }
    }
}
