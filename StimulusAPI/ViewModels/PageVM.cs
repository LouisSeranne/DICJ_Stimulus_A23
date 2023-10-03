using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class PageVM
    {
        public PageVM(int id, int pts, int ordre, int? noeudId)
        {
            Id = id;
            Pts = pts;
            Ordre = ordre;
            NoeudId = noeudId;
        }
        public PageVM(Page page)
        {
            Id=page.Id;
            Pts=page.Pts;
            Ordre=page.Ordre;
            NoeudId=page.NoeudId;
        }
        public PageVM()
        {

        }

        public int Id { get; set; }
        public int Pts { get; set; }
        public int Ordre { get; set; }
        public int? NoeudId { get; set; }
    }
}
