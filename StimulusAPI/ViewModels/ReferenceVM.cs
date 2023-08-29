using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class ReferenceVM : TheorieComponentVM
    {
        public ReferenceVM(int id, string url, string description, string typeComposant)
        {
            Id = id;
            Url = url;
            Description = description;
            TypeComposant = typeComposant;
        }
        public ReferenceVM(Reference reference)
        {
            Id = reference.Id;
            Url = reference.Url;
            Description = reference.Description;
            TypeComposant = "reference";
        }
        public ReferenceVM()
        {

        }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string TypeComposant {get; set;}
    }
}
