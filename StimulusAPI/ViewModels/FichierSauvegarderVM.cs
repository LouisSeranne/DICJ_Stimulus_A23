using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class FichierSauvegarderVM:TheorieComponentVM
    {
        public FichierSauvegarderVM(int id, int? progressionPageId, string? fichierEtudiantDa, string nom, string? contenu, DateTime? version)
        {
            Id = id;
            ProgressionPageId = progressionPageId;
            FichierEtudiantDa = fichierEtudiantDa;
            Nom = nom;
            Contenu = contenu;
            Version = version;
        }
        public FichierSauvegarderVM(FichierSauvegarde fichierSauvegarde)
        {
            Id=fichierSauvegarde.Id;
            ProgressionPageId = fichierSauvegarde.ProgressionPageId;
            FichierEtudiantDa = fichierSauvegarde.FichierEtudiantDa;
            Nom = fichierSauvegarde.Nom;
            Contenu = fichierSauvegarde.Contenu;
            Version = fichierSauvegarde.Version;
        }
        public FichierSauvegarderVM()
        {

        }

        public int Id { get; set; }
        public int? ProgressionPageId { get; set; }
        public string? FichierEtudiantDa { get; set; }
        public string? Contenu { get; set; }
        public string Nom { get; set; } = null!;
        public DateTime? Version { get; set; }
    }
}
