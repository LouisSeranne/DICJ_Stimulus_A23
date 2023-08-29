using Microsoft.EntityFrameworkCore.Query.Internal;
using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class VideoVM : TheorieComponentVM
    {
        public VideoVM(int id, int largeur, int longueur, string url, string? description, string typeComposant)
        {
            Id = id;
            Largeur = largeur;
            Longueur = longueur;
            Url = url;
            Description = description;
            TypeComposant = typeComposant;
        }
        public VideoVM(Video video)
        {
            Id = video.Id;    
            Largeur = video.Largeur;
            Longueur = video.Longeur;
            Url = video.Url;
            Description = video.Description;
            TypeComposant = "video";
        }
        public VideoVM()
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
