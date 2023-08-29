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
using InterpreteurPython;
using Serilog;

namespace StimulusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PageExerciceController : ControllerBase
{
    private readonly DevProjetStimulusContext _context;

    public PageExerciceController(DevProjetStimulusContext context)
    {
        _context = context;
    }
    // GET: api/Pages/5
    [HttpGet("{id}")]
    public async Task<ExerciceVM> GetPageExercice(int id)
    {
        var pageExerciceQuery = await _context.Exercices.Where(x => x.Id == id).FirstOrDefaultAsync();
        var fichierSourceQuery = await _context.FichierSources.Where(x => x.ExerciceId == id).ToListAsync();

        List<string> theorieComponentVMs = new List<string>();
        List<FichierSourceVM> fichierSourceVM = new List<FichierSourceVM>();
        ExerciceVM exerciceVM = new ExerciceVM();

        if (pageExerciceQuery != null)
            exerciceVM = new ExerciceVM(pageExerciceQuery);

        foreach (FichierSource fichier in fichierSourceQuery)
        {
            FichierSourceVM fichierVM = new FichierSourceVM(fichier);
            fichierSourceVM.Add(fichierVM);
        }

        exerciceVM.FichierSource = JsonConvert.SerializeObject(fichierSourceVM);

        return exerciceVM;
    }


    [HttpPost]
    public async Task<string> GetPythonResult(string codeJson, int idEtudiant)
    {
        return JsonConvert.SerializeObject(PythonReader.Run(JsonConvert.DeserializeObject<List<FichierPython>>(codeJson), idEtudiant.ToString()));
    }
}
