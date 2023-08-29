using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteurPython;

//Classe qui s'occupe de la gestion des fichiers temporaire pour la lecture du code de l'utilisateur. Elle est internal car son utilisation est limitée a l'interieur du DLL

internal class TempHandler
{
    private string projectDir, tempDirName, pathToTempDir;

    public string PathToTempDir { get => pathToTempDir; set => pathToTempDir = value; }

    //Le constructeur s'occupe de créer le dossier qui contient tout les dossier temporaire des etudiants
    public TempHandler()
    {
        projectDir = Directory.GetCurrentDirectory();
        tempDirName = "TempPythonFiles";
        PathToTempDir = Path.Combine(projectDir, tempDirName);
        CreateTempFolder(PathToTempDir);
    }

    //S'occupe de créer le fichier qui contient les fichiers temporaire s'il n'existe pas déja
    //Params: 
    //  path : chemin vers le dossier dans lequel le dossier doit être créé
    private void CreateTempFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /*Fonction qui s'occupe de créer le dossier et les fichiers temporaire
     *params:
     *  codes : List de FichierPython que l'étudiant envois pour faire interpreter
     *  userid : Id de l'utilisateur qui envois le code
     */
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


    //Fonction qui supprime le dossier temporaire, supprimant en meme temps les fichiers pythons
    public void TempCleanUp(string pathToUserTempFiles)
    {
        Directory.Delete(pathToUserTempFiles, true);
    }


    //Fonctions publique qui appelle la fonction privée pour la création des fichiers temporaires
    public string TempSetup(List<FichierPython> codes, string userId)
    {
        return CreateTempPythonFiles(codes, userId);
    }
}
