namespace StimulusFrontEnd.Models
{
    public class GrapheTemp
    {
        public int Id { get; set; }
        public string? Couleur { get; set; }
        public string Nom { get; set; } = null!;
        public int? GroupeId { get; set; }
        public string? StatusCode { get; set; }
    }
}
