using Microsoft.EntityFrameworkCore.Query.Internal;
using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class CodeVM : TheorieComponentVM
    {
        public CodeVM(int id,string titre, string contenu, string typeComposant)
        {
            Id = id;
            Titre = titre;
            Contenu = contenu;
            TypeComposant = typeComposant;
        }
        public CodeVM(Code code)
        {
            Id= code.Id;
            Titre = code.Titre;
            Contenu = code.Contenu;
            TypeComposant = "code";
        }
        public CodeVM()
        {

        }
        public int Id { get; set; }
        public string Contenu { get; set; }
        public string Titre { get; set; }
        public string TypeComposant { get; set; }
    }
}
