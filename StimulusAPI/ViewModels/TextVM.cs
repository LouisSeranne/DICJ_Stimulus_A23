using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class TextVM : TheorieComponentVM
    {
        public TextVM(int id, string content, string typeComposant)
        {
            Id = id;
            Contenu = content;
            TypeComposant = typeComposant;
        }
        public TextVM(TexteFormater texteFormater)
        {
            Id=texteFormater.Id;
            Contenu = texteFormater.Contenu;
            TypeComposant = "texte_formater";
        }
        public TextVM()
        {
        }

        public int Id { get; set; }
        public string Contenu { get; set; }
        public string TypeComposant { get; set; }
    }
}
