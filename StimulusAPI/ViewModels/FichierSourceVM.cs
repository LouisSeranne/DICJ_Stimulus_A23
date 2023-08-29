using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class FichierSourceVM:TheorieComponentVM
    {
        public FichierSourceVM(int id, string nom, string contenu, int exerciceID)
        {
            Id = id;
            Nom = nom;
            Contenu = contenu;
            ExerciceId = exerciceID;
        }
        public FichierSourceVM(FichierSource fichierSource)
        { 
            Id= fichierSource.Id;
            Nom = fichierSource.Nom;
            Contenu = fichierSource.Contenu;
            ExerciceId = fichierSource.ExerciceId;
        }
        public FichierSourceVM()
        {

        }
        public int Id { get; set; }
        public string Contenu { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public int? ExerciceId { get; set; }
    }
}
