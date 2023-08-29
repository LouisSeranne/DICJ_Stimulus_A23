using StimulusAPI.Models;


namespace StimulusAPI.ViewModels
{
    public class ImageVM : TheorieComponentVM
    {
        public ImageVM(int id, int largeur, int longueur, string url, string description, string typeComposant)
        {
            Id = id;
            Largeur = largeur;
            Longueur = longueur;
            Description = description;
            Url = url;
            TypeComposant = typeComposant;
        }
        public ImageVM(Image image)
        {
            Id=image.Id;
            Largeur=image.Largeur;
            Longueur=image.Longeur;    
            Description=image.Description;
            Url=image.Url;
            TypeComposant = "image";
        }
        public ImageVM()
        {

        }
        public int Id { get; set; }
        public int Largeur { get; set; }
        public int Longueur { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public string TypeComposant { get; set; }
    }
}
