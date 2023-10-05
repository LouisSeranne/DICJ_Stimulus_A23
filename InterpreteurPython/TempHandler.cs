using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteurPython;

/// <summary>
/// S'occupe des fichiers temporaires pour la lecture du code de l'utilisateur. Internal car utilisation limité au DLL
/// </summary>
internal class TempHandler
{
    private string projectDir, tempDirName, pathToTempDir;

    public string PathToTempDir { get => pathToTempDir; set => pathToTempDir = value; }


    /// <summary>
    /// Créer le dossier contentant tout les dossiers temporaires des étudiants
    /// </summary>
    public TempHandler()
    {
        projectDir = Directory.GetCurrentDirectory();
        tempDirName = "TempPythonFiles";
        PathToTempDir = Path.Combine(projectDir, tempDirName);
        CreateTempFolder(PathToTempDir);
    }

    /// <summary>
    /// S'occupe de créer le fichier qui contient les fichiers temporaire s'il n'existe pas déja
    /// </summary>
    /// <param name="path">Chemin vers le dossier dans lequel le dossier doit être créé</param>
    private void CreateTempFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// Créer le dossier et les fichiers temporaires
    /// </summary>
    /// <param name="codes">Liste envoyé par l'utilisateur pour l'interpréteur</param>
    /// <param name="userId">Id de l'utilisateur</param>
    /// <returns></returns>
    private string CreateTempPythonFiles(List<FichierPython> codes, string userId)
    {
        //Création du dossier temporaire dans lequel sera stocké les fichiers .py le temps qu'ils soient interprété
        string userIdString = "TempFiles" + userId;
        string pathToUserDir = Path.Combine(PathToTempDir, userIdString);
        CreateTempFolder(pathToUserDir);
        
        //Création des fichiers .py avec la List de FichierPython
        for (int i = 0; i < codes.Count; i++)
        {
            string tempFileName = codes[i].Nom;
            string pathToFile = Path.Combine(pathToUserDir, tempFileName);
            File.WriteAllTextAsync(pathToFile, codes[i].Contenu);
        }

        //Retourne le chemin vers le dossier de l'utilisateur
        return pathToUserDir;
    }

    /// <summary>
    /// Supprime le dossier temporaire, ainsi que les fichiers Python
    /// </summary>
    /// <param name="pathToUserTempFiles"></param>
    public void TempCleanUp(string pathToUserTempFiles)
    {
        Directory.Delete(pathToUserTempFiles, true);
    }


    /// <summary>
    /// Appelle la fonction CreateTempPythonFiles pour la création des fichiers temporaires
    /// </summary>
    /// <param name="codes"></param>
    /// <param name="userId"></param>
    /// <returns></returns>

#warning chercher pourquoi la fonction existe
    public string TempSetup(List<FichierPython> codes, string userId)
    {
        return CreateTempPythonFiles(codes, userId);
    }
}
