using System.Diagnostics;

namespace InterpreteurPython;


/// <summary>
/// Classe static qui s'occupe de lire le python. Pour faire fonctionner, seulement appeler PythonReader.Run() en fournissant les paramètres appropriés
/// </summary>
public static class PythonReader
{
    internal static TempHandler tempHandler = new TempHandler();

    /// <summary>
    /// Lit les fichiers Python créés par le tempHandler
    /// </summary>
    /// <param name="main">Fichier lu par le lecteur. Doit être main.py</param>
    /// <param name="pythonPath">Correspond au path de l'exécutable. Ne devrait pas être hardcodé et ne pas modifier la valeur car elle est propre au serveur IIS sur lequel le projet tourne</param>
    /// <returns></returns>
    static private string Read(string main)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        string pythonPath = Environment.GetEnvironmentVariable("StimulusPythonExecutable");

        //Donne le nom du processus
        start.FileName = pythonPath;

        //Les parametres du processus
        start.Arguments = main;

        //Sans console
        start.UseShellExecute = false;

        //Redirection de la sortie
        start.RedirectStandardOutput = true;

        //Ajouter le retour d'erreur
#warning oublie moi pas connard;
        start.RedirectStandardError = true;

        //start.Verb = "runas";
        using (Process process = Process.Start(start))
        {
            string result;
            string error;
            using (StreamReader reader = process.StandardOutput)
            {
                //Lis et retourne le résultat
                result = reader.ReadToEnd();
            }
            using (StreamReader reader2 = process.StandardError)
            {
                error = reader2.ReadToEnd();
            }

            string stringRemove = main.Replace(main.Split("\\").Last(), "");
            string errorMessage = error.Replace(stringRemove, "");

            return $"{result} {errorMessage}";
        }
    }

    /// <summary>
    /// Commande pour lancer la lecture du code Python
    /// </summary>
    /// <param name="code">Liste que l'utilisateur veut faire lire par l'itérateur</param>
    /// <param name="userId">DA de l'utilisateur</param>
    /// <returns></returns>
    static public string Run(List<FichierPython> code, string userId)
    {       
        //Appel la fonction qui setup les fichiers temporaire qui vont contenir le code de l'utilisateur
        string pathToUserTempFiles = tempHandler.TempSetup(code, userId);

        //Appel la fonction qui lit le code de l'utilisateur
        string result = Read(Path.Combine(pathToUserTempFiles, "main.py"));

        //Appel la fonction qui va supprimer les fichiers temporaires de l'utilisateur
        tempHandler.TempCleanUp(pathToUserTempFiles);

        //Retourne le résultat
        return result;
    }   
}