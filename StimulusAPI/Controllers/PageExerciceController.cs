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
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace StimulusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PageExerciceController : ControllerBase
{
    private readonly TestStimulusProjet_Evolution _context;

    public PageExerciceController(TestStimulusProjet_Evolution context)
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
        var log = Log.ForContext<StimulusAPI.Controllers.PageExerciceController>();
        DirectoryInfo dossier;
        StreamWriter fichier = null;
        string path = "Python/" + idEtudiant;
        bool status = false;
        try
        {
            JArray objetJson = JArray.Parse(codeJson);
            string code = (string)objetJson[0]["Contenu"];
            if (!Directory.Exists(path))
                dossier = Directory.CreateDirectory(path);
            fichier = new StreamWriter(path + "/main.py");
            fichier.Write(code);
            status = true;
        }
        catch (Exception e)
        {
            log.Information($"{e.Source} -> GetPythonResult(string codeJson = {codeJson}, int idEtudiant = {idEtudiant}): {e.Message}");
        }
        finally
        {
            fichier.Close();
        }

        if (status)
            await Start_Script(idEtudiant);

        return JsonConvert.SerializeObject(PythonReader.Run(JsonConvert.DeserializeObject<List<FichierPython>>(codeJson), idEtudiant.ToString()));
    }

    private async Task Start_Script(int id)
    {
#warning J'ai pas la moindre idée de ce que je fais
    }
}
