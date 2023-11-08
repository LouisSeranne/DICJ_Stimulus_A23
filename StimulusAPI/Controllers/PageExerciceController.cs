﻿using System.Linq;
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
using Microsoft.Extensions.Hosting;
using Renci.SshNet;
using System;
using System.IO;
using System.Text;
using NuGet.Protocol.Plugins;
using Radzen.Blazor.Rendering;
using Renci.SshNet.Common;

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

        var old = JsonConvert.SerializeObject(PythonReader.Run(JsonConvert.DeserializeObject<List<FichierPython>>(codeJson), idEtudiant.ToString()));
        var nouveau = System.IO.File.ReadAllText(path + $"/output_interpreteur_{idEtudiant}");
        log.Information($"Sortie attendue -> {old} ");
        log.Information($"Sortie obtenue -> {nouveau} ");
        return old;
    }

    private async Task Start_Script(int id)
    {
        string host = "10.10.10.100";
        string username = "tech";
        string password = "jambon1723!";

        string localMainPy = $"Python/{id}/main.py";
        string remoteMainPy = $"{id}/main.py";

        string localOutput = $"Python/{id}/output_interpreteur_{id}.txt";
        string remoteOutput = $"{id}/output_interpreteur_{id}.txt";

        try
        {
            using (SshClient client = new SshClient(host, username, password))
            {
                client.Connect();
                client.RunCommand($"cp -r base/ {id}/");

                using (var scp = new ScpClient(client.ConnectionInfo))
                {
                    scp.Connect();
                    using (var fileStream = new FileStream(localMainPy, FileMode.Open))
                    {
                        scp.Upload(fileStream, remoteMainPy);
                    }
                    scp.Disconnect();
                }

                client.RunCommand($"cd {id} && ./start.sh interpreteur_{id}");

                using (var scp = new ScpClient(client.ConnectionInfo))
                {
                    scp.Connect();
                    using (var fileStream = System.IO.File.Create(localOutput))
                    {
                        scp.Download(remoteOutput, fileStream);
                    }
                    scp.Disconnect();
                }

                client.RunCommand($"rm {id}/* && rmdir {id}");
                client.Disconnect();
            }
        }
        catch (Exception e )
        {
            var log = Log.ForContext<StimulusAPI.Controllers.PageExerciceController>();
            log.Information($"{e.Source} -> Start_Script(int id = {id}): {e.Message}");
        }
    }
}
