using StimulusAPI.Models;

namespace StimulusAPI.ViewModels
{
    public class LienUtileVM
    {
        public LienUtileVM(int id, string? url, int? grapheId, string? nomUrl)
        {
            Id = id;
            Url = url;
            GrapheId = grapheId;
            NomUrl = nomUrl;
        }
        public LienUtileVM(LienUtile lienUtile)
        {
            Id=lienUtile.Id;
            Url = lienUtile.Url;
            GrapheId = lienUtile.GrapheId;
            NomUrl = lienUtile.NomUrl;
        }
        public LienUtileVM()
        {

        }
        public int Id { get; set; }
        public string? Url { get; set; }
        public int? GrapheId { get; set; }
        public string? NomUrl { get; set; }

    }
}
