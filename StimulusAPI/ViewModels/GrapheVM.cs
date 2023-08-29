namespace StimulusAPI.ViewModels
{
    public class GrapheVM
    {
        public GrapheVM(Models.Graphe graphe)
        {
            Id = graphe.Id;
            Couleur = graphe.Couleur;
            Nom = graphe.Nom;
            GroupeId = graphe.GroupeId;
            StatusCode = graphe.StatusCode;
        }
        public int Id { get; set; }
        public string? Couleur { get; set; }
        public string Nom { get; set; } = null!;
        public int? GroupeId { get; set; }
        public string? StatusCode { get; set; }
    }
}
