using StimulusAPI.Models;



namespace StimulusAPI.ViewModels
{
    public class ExerciceVM : TheorieComponentVM
    {
        public ExerciceVM(int id, string solution, string typeComposant, string fichierSource)
        {
            Id = id;
            Solution = solution;
            TypeComposant = typeComposant;
            FichierSource = fichierSource;
        }
        public ExerciceVM(Exercice exercice)
        {
            Id = exercice.Id;
            Solution = exercice.Solution;
            TypeComposant = "exercice";
            FichierSource = "";
        }
        public ExerciceVM()
        {

        }

        public int Id { get; set; }
        public string Solution { get; set; }
        public string? FichierSource { get; set; }
        public string TypeComposant { get; set; }
    }
}