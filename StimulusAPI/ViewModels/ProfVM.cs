using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class ProfVM
    {
        public ProfVM(Professeur prof)
        {
            Id = prof.Id;
            Prenom = prof.Prenom;
            Nom = prof.Nom;
            MotDePasse = prof.MotDePasse;
            Groupes = new List<GroupeVM>();

            foreach(Groupe groupe in prof.Groupes)
            {
                Groupes.Add(new GroupeVM(groupe,1));
            }
        }

        public ProfVM(Professeur prof,int constructeur)
        {
            Id = prof.Id;
            Prenom = prof.Prenom;
            Nom = prof.Nom;
            MotDePasse = prof.MotDePasse;
        }

        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? MotDePasse { get; set; }

        public virtual List<GroupeVM> Groupes { get; set; }
    }
}
