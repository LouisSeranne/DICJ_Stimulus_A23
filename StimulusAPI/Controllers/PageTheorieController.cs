using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StimulusAPI.Context;
using StimulusAPI.Models;
using StimulusAPI.ViewModels;
using System.Text.Json;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StimulusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageTheorieController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public PageTheorieController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<List<string>> GetPageTheorie(int id)
        {
            var pageTheoryQuery = await _context.PageComposants.Where(x => x.PageId == id).OrderBy(x => x.Ordre).Select(x => new { x.IdReference, x.TypeComposant }).ToListAsync();

            List<string> theorieComponentVMs = new List<string>();

            foreach(object o in pageTheoryQuery)
            {
                Console.WriteLine(o.ToString());
                object typeComposant = o?.GetType()?.GetProperty("TypeComposant")?.GetValue(o, null);
                object composantID= o?.GetType()?.GetProperty("IdReference")?.GetValue(o, null);
                Console.WriteLine(typeComposant.ToString());

                if (typeComposant.ToString() == "texte_formater")
                {
                    TexteFormater text_formaterQuery = _context.TexteFormaters.Where(x => x.Id == (int)composantID).FirstOrDefault();
                    TextVM textVM = new TextVM(text_formaterQuery);
                    string textVmJson = JsonConvert.SerializeObject(textVM);
                    theorieComponentVMs.Add(textVmJson);
                }
                else if(typeComposant.ToString() == "image")
                {
                    Image imageQuery = _context.Images.Where(x => x.Id == (int)composantID).FirstOrDefault();
                    ImageVM imageVM = new ImageVM(imageQuery);
                    string imageVmJson = JsonConvert.SerializeObject(imageVM);
                    theorieComponentVMs.Add(imageVmJson);
                }
                else if (typeComposant.ToString() == "video")
                {
                    Video videoQuery = _context.Videos.Where(x=>x.Id== (int)composantID).FirstOrDefault();
                    VideoVM videoVM = new VideoVM(videoQuery);
                    string videoVmJson = JsonConvert.SerializeObject(videoVM);
                    theorieComponentVMs.Add(videoVmJson);
                }
                else if (typeComposant.ToString() == "reference")
                {
                    Reference referenceQuery = _context.References.Where(x => x.Id == (int)composantID).FirstOrDefault();
                    ReferenceVM referenceVM = new ReferenceVM(referenceQuery);
                    string referenceVmJson = JsonConvert.SerializeObject(referenceVM);
                    theorieComponentVMs.Add(referenceVmJson);
                }
                else if (typeComposant.ToString() == "code") 
                {
                    Code codeQuery = _context.Codes.Where(x => x.Id == (int)composantID).FirstOrDefault();
                    CodeVM codeVM = new CodeVM(codeQuery);
                    string codeVmJson = JsonConvert.SerializeObject(codeVM);
                    theorieComponentVMs.Add(codeVmJson);
                }
                else if (typeComposant.ToString() == "exercice")
                {
                    Exercice exerciceQuery = _context.Exercices.Where(x => x.Id == (int)composantID).FirstOrDefault();
                    ExerciceVM exerciceVM = new ExerciceVM(exerciceQuery);
                    string exerciceVmJson = JsonConvert.SerializeObject(exerciceVM);
                    theorieComponentVMs.Add(exerciceVmJson);
                }
            }
            foreach(string s in theorieComponentVMs)
            {
                Console.WriteLine(s);
                Console.WriteLine("\n");
            }
            return theorieComponentVMs;
        }

    }
}
