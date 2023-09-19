using StimulusAPI.Models;
using System.Runtime.CompilerServices;

namespace StimulusAPI.ViewModels
{
    public class PageVM
    {
        public PageVM(int id, int pts, int ordre, int? noeudId, string nom)
        {
            Id = id;
            Pts = pts;
            Ordre = ordre;
            NoeudId = noeudId;
            Nom = nom;
        }
        public PageVM(Page page)
        {
            Id=page.Id;
            Pts=page.Pts;
            Ordre=page.Ordre;
            NoeudId=page.NoeudId;
            Nom = page.Nom;
        }
        public PageVM()
        {

        }

        public int Id { get; set; }
        public int Pts { get; set; }
        public int Ordre { get; set; }
        public int? NoeudId { get; set; }
        public string Nom { get; set; }
    }
}
