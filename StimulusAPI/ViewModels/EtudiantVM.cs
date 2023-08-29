using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class EtudiantVM
    {
        public EtudiantVM(Etudiant etu)
        {
            CodeDa = etu.CodeDa;
            Nom = etu.Nom;
            Prenom = etu.Prenom;
            MotDePasse = etu.MotDePasse;
            Groupes = new List<GroupeVM>();

            foreach(Groupe groupe in etu.Groupes)
            {
                Groupes.Add(new GroupeVM(groupe,1));
            }
        }

        public EtudiantVM(Etudiant etu,int constructeur)
        {
            CodeDa = etu.CodeDa;
            Nom = etu.Nom;
            Prenom = etu.Nom;
            MotDePasse = etu.MotDePasse;
        }

        public string CodeDa { get; set; } = null!;
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? MotDePasse { get; set; }

        public virtual List<GroupeVM> Groupes { get; set; }
    }
}
