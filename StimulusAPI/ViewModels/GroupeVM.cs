using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class GroupeVM
    {
        public GroupeVM(Groupe groupe)
        {
            Id = groupe.Id;
            Nom = groupe.Nom;
            CoursId = groupe.CoursId;
            ProfesseurId = groupe.ProfesseurId;
            Etudiants = new List<EtudiantVM>();
            Professeurs = new List<ProfVM>();

            foreach (Etudiant etudiant in groupe.EtudiantDa)
            {
                Etudiants.Add(new EtudiantVM(etudiant, 1));
            }

            foreach (Professeur professeur in groupe.ProfId)
            {
                Professeurs.Add(new ProfVM(professeur, 1));
            }
        }

        public GroupeVM(Groupe groupe,int constructeur)
        {
            Id = groupe.Id;
            Nom = groupe.Nom;
            CoursId = groupe.CoursId;
            ProfesseurId = groupe.ProfesseurId;
        }

        public int Id { get; set; }
        public string? Nom { get; set; }
        public int? CoursId { get; set; }
        public int? ProfesseurId { get; set; }

        public virtual List<EtudiantVM> Etudiants { get; set; }
        public virtual List<ProfVM> Professeurs { get; set; }
    }
}
